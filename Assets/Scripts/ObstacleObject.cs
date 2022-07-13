using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public Obstacle data;
    TileObject refTile;

    /// <summary>
    /// This is a method that it is called whenever the item has been clicked or tapped.
    /// Works on Mobile an PC
    /// </summary>
    private void OnMouseDown()
    {
        // Debug.Log("clicked on " + gameObject.name);
        bool usedResource = false;

        // we can call directly the method that adds the resource
        switch (data.obstacleType)
        {
            case Obstacle.ObstacleType.Wood:
                usedResource = ResourceManager.Instance.AddWood(data.resourceAmount);
                break;

            case Obstacle.ObstacleType.Rock:
                usedResource = ResourceManager.Instance.AddStone(data.resourceAmount);
                break;
        }

        if (usedResource)
        {
            refTile.data.CleanTile();
            Destroy(gameObject);
        }
        else
        {
            Debug.Log("can not destroy cause inventory is full");
        }
    }

    public void SetTileReference(TileObject obj)
    {
        refTile = obj;
    }
}
