using System;
using System.Collections;
using System.Collections.Generic;
using CityBuilder.SO;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] SO_MapConfig mapConfig;
    Grid placementGrid;

    private void Start()
    {
        placementGrid = new Grid(mapConfig.width, mapConfig.height);
    }

    public bool CheckIfPositionInBound(Vector3Int position)
    {
        if (position.x >= 0 && position.x < mapConfig.width && position.z >= 0 && position.z < mapConfig.height)
        {
            return true;
        }

        return false;
    }

    public bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    private bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }

    public void PlaceTemporaryStructure(Vector3Int position, GameObject roadStraight, CellType type)
    {
        placementGrid[position.x, position.z] = type;
        GameObject newStructure = Instantiate(roadStraight, position, Quaternion.identity);
    }
}
