using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiDirector : MonoBehaviour
    {
        public PlacementManager placementManager;
        [Space(10)]
        public Transform pedestriantGroup;
        public GameObject pedestrianPrefab;
        [Space(10)]
        public Transform carGroup;
        public GameObject carPrefab;

        AiAdjacencyGraph graph = new AiAdjacencyGraph();

        public void SpawnAllAgents()
        {
            foreach (var house in placementManager.GetAllHouseStructure())
            {
                TrySpawningAnAgent(house, placementManager.GetRandomSpecialStructure());
            }

            foreach (var special in placementManager.GetAllSpecialStructure())
            {
                TrySpawningAnAgent(special, placementManager.GetRandomHouseStructure());
            }
        }

        private void TrySpawningAnAgent(StructureModel startStructure, StructureModel endStructure)
        {
            if (startStructure != null && endStructure != null)
            {
                var startPosition = ((INeedingRoad)startStructure).RoadPosition;
                var endPosition = ((INeedingRoad)endStructure).RoadPosition;

                if (startPosition == endPosition)
                {
                    Debug.Log($"skip spawn agent at same position {startPosition}");
                    return;
                }

                var startMarker = placementManager.GetStructureAt(startPosition).GetPedestrianSpawnMarker(startStructure.transform.position);
                var endMarker = placementManager.GetStructureAt(endPosition).GetPedestrianSpawnMarker(endStructure.transform.position);

                var path = placementManager.GetPathBetween(startPosition, endPosition, true);

                if (path.Count > 0)
                {
                    path.Reverse();

                    List<Vector3> agentPath = GetPedestrianPath(path, startMarker.Position, endMarker.Position);

                    var agent = Instantiate(pedestrianPrefab, startMarker.Position, Quaternion.identity);
                    agent.transform.SetParent(pedestriantGroup);
                    var aiAgent = agent.GetComponentInChildren<AiAgent>();
                    aiAgent.Initialize(agentPath);
                }
            }
        }

        private List<Vector3> GetPedestrianPath(List<Vector3Int> path, Vector3 startPosition, Vector3 endPosition)
        {
            graph.ClearGraph();

            CreateGraph(path);
            // Debug.Log(graph);

            return AiAdjacencyGraph.AStarSearch(graph, startPosition, endPosition);
        }

        private void CreateGraph(List<Vector3Int> path)
        {
            Dictionary<AiRoadMarker, AiRoadMarker> tempDictionary = new Dictionary<AiRoadMarker, AiRoadMarker>();

            for (int i = 0; i < path.Count; i++)
            {
                var currentPosition = path[i];
                var roadStructure = placementManager.GetStructureAt(currentPosition);

                var markerList = roadStructure.GetPedestrianMarkers();

                bool limitDistance = markerList.Count == 4;
                tempDictionary.Clear();

                foreach (var marker in markerList)
                {
                    graph.AddVertex(marker.Position);
                    foreach (var markerNeighbourPosition in marker.GetAdjacentPositions())
                    {
                        graph.AddEdge(marker.Position, markerNeighbourPosition);
                    }

                    if (marker.OpenForConnection && i + 1 < path.Count)
                    {
                        var nextRoadStructure = placementManager.GetStructureAt(path[i + 1]);
                        if (limitDistance)
                        {
                            tempDictionary.Add(marker, nextRoadStructure.GetPedestrianSpawnMarker(marker.Position));
                        }
                        else
                        {
                            graph.AddEdge(marker.Position, nextRoadStructure.GetNearestMarkerTo(marker.Position).Position);
                            
                            marker.ConnectToMarker(nextRoadStructure.GetNearestMarkerTo(marker.Position));
                        }
                    }
                }

                if (limitDistance && tempDictionary.Count == 4)
                {
                    var distanceSortedMarkers = tempDictionary.OrderBy(x => Vector3.Distance(x.Key.Position, x.Value.Position)).ToList();
                    for (int j = 0; j<2; j++)
                    {
                        graph.AddEdge(distanceSortedMarkers[j].Key.Position, distanceSortedMarkers[j].Value.Position);

                        distanceSortedMarkers[j].Key.ConnectToMarker(distanceSortedMarkers[j].Value);
                    }
                }
            }
        }

        // private void Update() {
        //     foreach (var vertex in graph.GetVertices())
        //     {
        //         foreach (var vertexNeighbour in graph.GetConnectedVerticesTo(vertex))
        //         {
        //             Debug.DrawLine(vertex.Position + Vector3.up, vertexNeighbour.Position + Vector3.up, Color.red);
        //         }
        //     }
        // }
    }
}
