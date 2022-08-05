using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiDirector : MonoBehaviour
    {
        public PlacementManager placementManager;
        public Transform pedestriantGroup;
        public GameObject[] pedestrianPrefabs;

        public void SpawnAllAgents()
        {
            foreach(var house in placementManager.GetAllHouseStructure())
            {
                TrySpawningAnAgent(house, placementManager.GetRandomSpecialStructure());
            }

            foreach(var special in placementManager.GetAllSpecialStructure())
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
                
                var path = placementManager.GetPathBetween(startPosition, endPosition, true);

                if (path.Count > 0)
                {
                    path.Reverse();
                    var agent = Instantiate(GetRandomPedestriant(), startPosition, Quaternion.identity);
                    agent.transform.SetParent(pedestriantGroup);
                    var aiAgent = agent.GetComponent<AiAgent>();
                    aiAgent.Initialize(path);
                }
            }
        }

        private GameObject GetRandomPedestriant()
        {
            return pedestrianPrefabs[UnityEngine.Random.Range(0, pedestrianPrefabs.Length)];
        }
    }
}
