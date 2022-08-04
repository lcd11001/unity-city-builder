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
        placementManager.ModifyStructureModel(position, fourWay, Quaternion.identity);
    }

    private void Create3Way(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        if (result[Dir.TOP] == CellType.Road && result[Dir.RIGHT] == CellType.Road && result[Dir.DOWN] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, threeWay, Quaternion.identity);
        }
        else if (result[Dir.RIGHT] == CellType.Road && result[Dir.DOWN] == CellType.Road && result[Dir.LEFT] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, threeWay, Quaternion.Euler(0, 90, 0));
        }
        else if (result[Dir.DOWN] == CellType.Road && result[Dir.LEFT] == CellType.Road && result[Dir.TOP] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, threeWay, Quaternion.Euler(0, 180, 0));
        }
        else if (result[Dir.LEFT] == CellType.Road && result[Dir.TOP] == CellType.Road && result[Dir.RIGHT] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, threeWay, Quaternion.Euler(0, 270, 0));
        }
    }

    private void CreateCorner(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        if (result[Dir.TOP] == CellType.Road && result[Dir.RIGHT] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, corner, Quaternion.Euler(0, 90, 0));
        }
        else if (result[Dir.RIGHT] == CellType.Road && result[Dir.DOWN] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, corner, Quaternion.Euler(0, 180, 0));
        }
        else if (result[Dir.DOWN] == CellType.Road && result[Dir.LEFT] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, corner, Quaternion.Euler(0, 270, 0));
        }
        else if (result[Dir.LEFT] == CellType.Road && result[Dir.TOP] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, corner, Quaternion.identity);
        }
    }

    private bool CreateStraightRoad(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        if (result[Dir.LEFT] == CellType.Road && result[Dir.RIGHT] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, roadStraight, Quaternion.identity);
            return true;
        }
        else if (result[Dir.TOP] == CellType.Road && result[Dir.DOWN] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, roadStraight, Quaternion.Euler(0, 90, 0));
            return true;
        }

        return false;
    }

    private void CreateDeadEnd(PlacementManager placementManager, CellType[] result, Vector3Int position)
    {
        if (result[Dir.TOP] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, deadEnd, Quaternion.Euler(0, 270, 0));
        }
        else if (result[Dir.RIGHT] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, deadEnd, Quaternion.identity);
        }
        else if (result[Dir.DOWN] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, deadEnd, Quaternion.Euler(0, 90, 0));
        }
        else if (result[Dir.LEFT] == CellType.Road)
        {
            placementManager.ModifyStructureModel(position, deadEnd, Quaternion.Euler(0, 180, 0));
        }
    }
}
