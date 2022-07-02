using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile 
{
    // the building referrece
    public Building buildingRef;
    // Tile is occupied by something (building, resource, ...)
    public bool occupied;
    public ObstacleType obstacleType = ObstacleType.None;
    // the stuff that  the tile is being occupied by
    public enum ObstacleType
    {
        None,
        Resource,
        Building
    }

    public void SetOccupied(ObstacleType t)
    {
        occupied = true;
        obstacleType = t;
    }

    public void SetOccupied(ObstacleType t, Building b)
    {
        occupied = true;
        obstacleType = t;
        buildingRef = b;
    }

    public void CleanTile()
    {
        occupied = false;
        obstacleType = ObstacleType.None;
        // LCD: missing ??? 
        // buildingRef = null;
    }
}
