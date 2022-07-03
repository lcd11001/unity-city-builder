using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tile
{
    // the building referrece
    public Building buildingRef;
    // Tile is occupied by something (building, resource, ...)
    public ObstacleType obstacleType = ObstacleType.None;

    bool isStarterTile = true;

    // the stuff that  the tile is being occupied by
    public enum ObstacleType
    {
        None,
        Resource,
        Building
    }

    #region Methods

    public void SetOccupied(ObstacleType t)
    {
        obstacleType = t;
    }

    public void SetOccupied(ObstacleType t, Building b)
    {
        obstacleType = t;
        buildingRef = b;
    }

    public void CleanTile()
    {
        obstacleType = ObstacleType.None;
        // LCD: missing ??? 
        // buildingRef = null;
    }

    public void StarterTileValue(bool value)
    {
        isStarterTile = value;
    }

    #endregion

    #region Booleans
    public bool IsOccupied
    {
        get
        {
            return obstacleType != ObstacleType.None;
        }
    }

    public bool CanSpawnObstacle
    {
        get
        {
            return !isStarterTile;
        }
    }
    #endregion
}
