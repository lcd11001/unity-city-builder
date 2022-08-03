using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadFixer : MonoBehaviour
{
    public GameObject deadEnd, roadStraight, corner, threeWay, fourWay;

    public void FixRoadAtPosition(PlacementManager placementManager, Vector3Int position)
    {
        // [Left, Top, Right, Down]
        var result = placementManager.GetNeighbourTypesFor(position);
        int roadCount = result.Where(x => x == CellType.Road).Count();
        if (roadCount == 0 || roadCount == 1)
        {
            CreateDeadEnd(placementManager, result, position);
        }
        else if (roadCount == 2)
        {
            if (CreateStraightRoad(placementManager, result, position))
            {
                return;
            }
            CreateCorner(placementManager, result, position);
        }
        else if (roadCount == 3)
        {
            Create3Way(placementManager, result, position);
        }
        else
        {
            Create4Way(placementManager, result, position);
        }
    }

    private void Create4Way(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        throw new NotImplementedException();
    }

    private void Create3Way(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        throw new NotImplementedException();
    }

    private void CreateCorner(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        throw new NotImplementedException();
    }

    private bool CreateStraightRoad(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        throw new NotImplementedException();
    }

    private void CreateDeadEnd(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        throw new NotImplementedException();
    }
}
