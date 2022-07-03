using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public ObstacleType obstacleType;
    public int resourceAmount = 10;

    /// <summary>
    /// This is a method that it is called whenever the item has been clicked or tapped.
    /// Works on Mobile an PC
    /// </summary>
    private void OnMouseDown()
    {
        Debug.Log("clicked on " + gameObject.name);

        // we can call directly the method that adds the resource
        switch (obstacleType)
        {
            case ObstacleType.Wood:
                ResourceManager.Instance.AddWood(resourceAmount);
                break;

            case ObstacleType.Rock:
                ResourceManager.Instance.AddStone(resourceAmount);
                break;
        }

        Destroy(gameObject);
    }

    public enum ObstacleType
    {
        Wood,
        Rock
    }
}
