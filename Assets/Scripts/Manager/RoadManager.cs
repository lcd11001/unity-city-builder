using System;
using System.Collections;
using System.Collections.Generic;
using SVS;
using UnityEngine;

[RequireComponent(typeof(RoadFixer))]
public class RoadManager : MonoBehaviour
{
    public PlacementManager placementManager;
    public List<Vector3Int> tempPlacementPosition = new List<Vector3Int>();
    // public List<Vector3Int> neighbourPositionToCheck = new List<Vector3Int>();
    public HashSet<Vector3Int> neighboursPositionToCheck = new HashSet<Vector3Int>();

    private Vector3Int startPosition;
    private bool placementMode = false;

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

        if (placementMode == false)
        {
            placementMode = true;
            startPosition = position;

            neighboursPositionToCheck.Clear();
            tempPlacementPosition.Clear();

            tempPlacementPosition.Add(position);

            placementManager.PlaceTemporaryStructure(position, roadFixer.deadEnd, CellType.Road);
        }
        else
        {
            placementManager.RemoveAllTemporaryStructures();

            tempPlacementPosition.Clear();
            // fix previous road
            FixRoadPrefabs();
            neighboursPositionToCheck.Clear();

            tempPlacementPosition = placementManager.GetPathBetween(startPosition, position);
            foreach (var tempPosition in tempPlacementPosition)
            {
                if (placementManager.CheckIfPositionIsFree(tempPosition) == false)
                {
                    continue;
                }
                placementManager.PlaceTemporaryStructure(tempPosition, roadFixer.deadEnd, CellType.Road);
            }
        }

        // fix current road
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
                // use hashset to avoid duplicate checking exist position
                neighboursPositionToCheck.Add(neighbour);
            }
        }

        foreach (var neighbourPosition in neighboursPositionToCheck)
        {
            roadFixer.FixRoadAtPosition(placementManager, neighbourPosition);
        }
    }

    public void FinishPlacingRoad()
    {
        placementMode = false;
        placementManager.SaveTemporaryStructures();
        if (tempPlacementPosition.Count > 0)
        {
            AudioPlayer.instance.PlayPlacementSound();
        }
        tempPlacementPosition.Clear();
        startPosition = Vector3Int.zero;
    }
}
