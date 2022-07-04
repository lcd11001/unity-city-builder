using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Obstacle
{
    public ObstacleType obstacleType;
    public int resourceAmount = 10;

    public enum ObstacleType
    {
        Wood,
        Rock
    }
}
