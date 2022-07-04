using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : MonoBehaviour
{
    public Obstacle data;

    /// <summary>
    /// This is a method that it is called whenever the item has been clicked or tapped.
    /// Works on Mobile an PC
    /// </summary>
    private void OnMouseDown()
    {
        Debug.Log("clicked on " + gameObject.name);

        // we can call directly the method that adds the resource
        switch (data.obstacleType)
        {
            case Obstacle.ObstacleType.Wood:
                ResourceManager.Instance.AddWood(data.resourceAmount);
                break;

            case Obstacle.ObstacleType.Rock:
                ResourceManager.Instance.AddStone(data.resourceAmount);
                break;
        }

        Destroy(gameObject);
    }

    
}
