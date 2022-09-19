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

        AiAdjacencyGraph pedestrianGraph = new AiAdjacencyGraph();
        AiAdjacencyGraph carGraph = new AiAdjacencyGraph();

        public void SpawnAllCars()
        {
            foreach (var house in placementManager.GetAllHouseStructure())
            {
                TrySpawningACar(house, placementManager.GetRandomSpecialStructure());
            }

            // foreach (var special in placementManager.GetAllSpecialStructure())
            // {
            //     TrySpawningACar(special, placementManager.GetRandomHouseStructure());
            // }
        }

        private void TrySpawningACar(StructureModel startStructure, StructureModel endStructure)
        {
            if (startStructure != null && endStructure != null)
            {
                var startPosition = ((INeedingRoad)startStructure).RoadPosition;
                var endPosition = ((INeedingRoad)endStructure).RoadPosition;

                if (startPosition == endPosition)
                {
                    Debug.Log($"skip spawn car at same position {startPosition}");
                    return;
                }

                // var startMarker = placementManager.GetStructureAt(startPosition).GetPedestrianSpawnMarker(startStructure.transform.position);
                // var endMarker = placementManager.GetStructureAt(endPosition).GetPedestrianSpawnMarker(endStructure.transform.position);

                var path = placementManager.GetPathBetween(startPosition, endPosition, true);

                if (path.Count > 0)
                {
                    path.Reverse();

                    // List<Vector3> agentPath = GetPedestrianPath(path, startMarker.Position, endMarker.Position);

                    // var car = Instantiate(carPrefab, startMarker.Position, Quaternion.identity);
                    var car = Instantiate(carPrefab, startPosition, Quaternion.identity);
                    car.transform.SetParent(carGroup);
                    var aiCar = car.GetComponent<AiCar>();
                    // aiCar.SetPath(agentPath);
                    aiCar.SetPath(path.ConvertAll(p => (Vector3)p));
                }
            }
        }

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
            pedestrianGraph.ClearGraph();

            CreatePedestrianGraph(path);
            // Debug.Log(pedestrianGraph);

            return AiAdjacencyGraph.AStarSearch(pedestrianGraph, startPosition, endPosition);
        }

        private void CreatePedestrianGraph(List<Vector3Int> path)
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
                    pedestrianGraph.AddVertex(marker.Position);
                    foreach (var markerNeighbourPosition in marker.GetAdjacentPositions())
                    {
                        pedestrianGraph.AddEdge(marker.Position, markerNeighbourPosition);
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
                            pedestrianGraph.AddEdge(marker.Position, nextRoadStructure.GetNearestPedestrianMarkerTo(marker.Position).Position);
                            
                            marker.ConnectToMarker(nextRoadStructure.GetNearestPedestrianMarkerTo(marker.Position));
                        }
                    }
                }

                if (limitDistance && tempDictionary.Count == 4)
                {
                    var distanceSortedMarkers = tempDictionary.OrderBy(x => Vector3.Distance(x.Key.Position, x.Value.Position)).ToList();
                    for (int j = 0; j<2; j++)
                    {
                        pedestrianGraph.AddEdge(distanceSortedMarkers[j].Key.Position, distanceSortedMarkers[j].Value.Position);

                        distanceSortedMarkers[j].Key.ConnectToMarker(distanceSortedMarkers[j].Value);
                    }
                }
            }
        }

        private List<Vector3> GetCarPath(List<Vector3Int> path, Vector3 startPosition, Vector3 endPosition)
        {
            carGraph.ClearGraph();

            CreateCarGraph(path);
            // Debug.Log(carGraph);

            return AiAdjacencyGraph.AStarSearch(carGraph, startPosition, endPosition);
        }

        private void CreateCarGraph(List<Vector3Int> path)
        {
            for (int i = 0; i < path.Count; i++)
            {
                var currentPosition = path[i];
                var roadStructure = placementManager.GetStructureAt(currentPosition);
                var markerList = roadStructure.GetCarMarkers();

                foreach (var marker in markerList)
                {
                    carGraph.AddVertex(marker.Position);
                    foreach (var markerNeighbour in marker.adjacentMarkers)
                    {
                        carGraph.AddEdge(marker.Position, markerNeighbour.Position);
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
