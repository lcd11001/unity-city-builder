using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(RoadFixer))]
public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public List<Vector3Int> tempPlacementPosition = new List<Vector3Int>();
    // public List<Vector3Int> neighbourPositionToCheck = new List<Vector3Int>();
    public HashSet<Vector3Int> neighbourPositionToCheck = new HashSet<Vector3Int>();
    public GameObject roadStraight;
    private RoadFixer roadFixer;

    private void Start()
    {
        roadFixer = GetComponent<RoadFixer>();
    }

    public void PlaceRoad(Vector3Int position)
    {
        if (placementManager.CheckIfPositionInBound(position) == false)
        {
            // out side the grid, not the Unity map
            return;
        }

        if (placementManager.CheckIfPositionIsFree(position) == false)
        {
            return;
        }

        tempPlacementPosition.Clear();
        tempPlacementPosition.Add(position);

        placementManager.PlaceTemporaryStructure(position, roadStraight, CellType.Road);

        FixRoadPrefabs();
    }

    private void FixRoadPrefabs()
    {
        foreach (var tempPosition in tempPlacementPosition)
        {
            roadFixer.FixRoadAtPosition(placementManager, tempPosition);

            var neighbours = placementManager.GetNeighbourOfTypeFor(tempPosition, CellType.Road);
            // neighbourPositionToCheck.AddRange(neighbours);
            foreach (var neighbour in neighbours)
            {
                neighbourPositionToCheck.Add(neighbour);
            }
        }

        foreach (var neighbourPosition in neighbourPositionToCheck)
        {
            roadFixer.FixRoadAtPosition(placementManager, neighbourPosition);
        }
    }
}
