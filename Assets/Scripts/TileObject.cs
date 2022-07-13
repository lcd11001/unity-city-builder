using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileObject : MonoBehaviour
{
    public Tile data;

    [Header("World Tile Data")]
    [Space(8)]
    public int xPos = 0;
    public int zPos = 0;

    private void OnMouseDown()
    {
        // Debug.Log("Clicked on " + gameObject.name);
        if (!data.IsOccupied)
        {
            if (GameManager.Instance.buildingToPlace != null)
            {
                if (CheckPlaceBuildingAvailable(GameManager.Instance.buildingToPlace))
                {
                    BuildingObject building = GameManager.Instance.SpawnBuilding(GameManager.Instance.buildingToPlace, this);
                    // data.SetOccupied(Tile.ObstacleType.Building, building.data);
                    GameManager.Instance.UpdateGridBuilding(xPos, zPos, building);
                }
                else
                {
                    Debug.Log("Not enough space for this building");
                }
            }
            else
            {
                Debug.Log("building to place is null");
            }
        }
        else
        {
            Debug.Log("This tile is occupied by " + data.obstacleType);
            if (data.buildingRef != null)
            {
                Debug.Log("   - Building " + data.buildingRef.buildingModel.gameObject.name);
            }
        }
    }

    private bool CheckPlaceBuildingAvailable(BuildingObject building)
    {
        for (int x = xPos; x < xPos + building.data.width; x++)
        {
            for (int z = zPos; z < zPos + building.data.length; z++)
            {
                if (GameManager.Instance.tileGrid[x, z].data.IsOccupied)
                {
                    return false;
                }
            }
        }
        return true;
    }
}
