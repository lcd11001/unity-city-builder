using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiAdjacencyGraph
    {
        Dictionary<AiVertex, List<AiVertex>> adjacencyDictionary = new Dictionary<AiVertex, List<AiVertex>>();

        public AiVertex AddVertex(Vector3 position)
        {
            if (GetVertexAt(position) != null)
            {
                return null;
            }

            AiVertex v = new AiVertex(position);
            AddVertex(v);
            return v;
        }

        private void AddVertex(AiVertex v)
        {
            if (adjacencyDictionary.ContainsKey(v))
            {
                return;
            }

            adjacencyDictionary.Add(v, new List<AiVertex>());
        }

        private AiVertex GetVertexAt(Vector3 position)
        {
            return adjacencyDictionary.Keys.FirstOrDefault(x => CompareVertices(position, x.Position));
        }

        private bool CompareVertices(Vector3 position1, Vector3 position2)
        {
            return Vector3.SqrMagnitude(position1 - position2) < 0.0001f;
        }

        public void AddEdge(Vector3 position1, Vector3 position2)
        {
            if (CompareVertices(position1, position2))
            {
                return;
            }

            var v1 = GetVertexAt(position1) ?? new AiVertex(position1);
            var v2 = GetVertexAt(position2) ?? new AiVertex(position2);

            AddEdgeBetween(v1, v2);
            AddEdgeBetween(v2, v1);
        }

        private void AddEdgeBetween(AiVertex v1, AiVertex v2)
        {
            if (v1 == v2)
            {
                return;
            }
            if (adjacencyDictionary.ContainsKey(v1))
            {
                if (adjacencyDictionary[v1].FirstOrDefault(x => x == v2) == null)
                {
                    adjacencyDictionary[v1].Add(v2);
                }
            }
            else
            {
                AddVertex(v1);
                adjacencyDictionary[v1].Add(v2);
            }
        }

        public List<AiVertex> GetConnectedVerticesTo(AiVertex v1)
        {
            if (adjacencyDictionary.ContainsKey(v1))
            {
                return adjacencyDictionary[v1];
            }

            return null;
        }

        public List<AiVertex> GetConnectedVerticesTo(Vector3 position)
        {
            var v1 = GetVertexAt(position);
            if (v1 == null)
            {
                return null;
            }

            return GetConnectedVerticesTo(v1);
        }

        public void ClearGraph()
        {
            adjacencyDictionary.Clear();
        }

        public IEnumerable<AiVertex> GetVertices()
        {
            return adjacencyDictionary.Keys;
        }

        public override string ToString()
        {
            StringBuilder builder = new StringBuilder();
            foreach (var vertex in adjacencyDictionary.Keys)
            {
                builder.AppendLine($"Vertex {vertex.ToString()} neighbours: {String.Join(",", adjacencyDictionary[vertex])}");
            }

            return builder.ToString();
        }

        public static List<Vector3> AStarSearch(AiAdjacencyGraph graph, Vector3 startPosition, Vector3 endPosition)
        {
            List<Vector3> path = new List<Vector3>();

            AiVertex start = graph.GetVertexAt(startPosition);
            AiVertex end = graph.GetVertexAt(endPosition);

            List<AiVertex> positionsTocheck = new List<AiVertex>();
            Dictionary<AiVertex, float> costDictionary = new Dictionary<AiVertex, float>();
            Dictionary<AiVertex, float> priorityDictionary = new Dictionary<AiVertex, float>();
            Dictionary<AiVertex, AiVertex> parentsDictionary = new Dictionary<AiVertex, AiVertex>();

            positionsTocheck.Add(start);
            priorityDictionary.Add(start, 0);
            costDictionary.Add(start, 0);
            parentsDictionary.Add(start, null);

            while (positionsTocheck.Count > 0)
            {
                AiVertex current = GetClosestVertex(positionsTocheck, priorityDictionary);
                positionsTocheck.Remove(current);
                if (current.Equals(end))
                {
                    path = GeneratePath(parentsDictionary, current);
                    return path;
                }

                foreach (AiVertex neighbour in graph.GetConnectedVerticesTo(current))
                {
                    float newCost = costDictionary[current] + graph.GetCostOfEnteringVertex(neighbour);
                    if (!costDictionary.ContainsKey(neighbour) || newCost < costDictionary[neighbour])
                    {
                        costDictionary[neighbour] = newCost;

                        float priority = newCost + ManhattanDiscance(end, neighbour);
                        positionsTocheck.Add(neighbour);
                        priorityDictionary[neighbour] = priority;

                        parentsDictionary[neighbour] = current;
                    }
                }
            }
            return path;
        }

        private float GetCostOfEnteringVertex(AiVertex neighbour)
        {
            return 1.0f;
        }

        private static AiVertex GetClosestVertex(List<AiVertex> list, Dictionary<AiVertex, float> distanceMap)
        {
            AiVertex candidate = list[0];
            foreach (AiVertex vertex in list)
            {
                if (distanceMap[vertex] < distanceMap[candidate])
                {
                    candidate = vertex;
                }
            }
            return candidate;
        }

        private static float ManhattanDiscance(AiVertex endVertex, AiVertex startVertex)
        {
            return Math.Abs(endVertex.Position.x - startVertex.Position.x) + Math.Abs(endVertex.Position.z - startVertex.Position.z);
        }

        public static List<Vector3> GeneratePath(Dictionary<AiVertex, AiVertex> parentMap, AiVertex endState)
        {
            List<Vector3> path = new List<Vector3>();
            AiVertex parent = endState;
            while (parent != null && parentMap.ContainsKey(parent))
            {
                path.Add(parent.Position);
                parent = parentMap[parent];
            }

            path.Reverse();
            return path;
        }
    }
}
