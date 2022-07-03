using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [Header("Builder")]
    [Space(8)]
    public GameObject tilePrefab;
    public Transform tilesHolder;
    public float tileSize = 1.0f;
    public float tileEndHeight = 1.0f;
    public int levelWidth;
    public int levelLength;

    [Header("Resources")]
    [Space(8)]
    public Transform resourcesHolder;
    public GameObject woodPrefab;
    public GameObject rockPrefab;
    [Range(0, 1)]
    public float obstacleChance = 0.3f;

    public int xBounds = 3;
    public int zBounds = 3;

    private void Start()
    {
        CreateLevel();
    }

    /// <summary>
    /// Create out grid depending on our level width and length
    /// </summary>
    public void CreateLevel()
    {
        for (int x = 0; x < levelWidth; x++)
        {
            for (int z = 0; z < levelLength; z++)
            {
                TileObject spawnedTile = SpawnTile(x * tileSize, z * tileSize);

                bool obstacleX = (x < xBounds || x > levelWidth - xBounds - 1);
                bool obstacleZ = (z < zBounds || z > levelLength - zBounds - 1);

                if (obstacleX || obstacleZ)
                {
                    // we can spawn an obstacle in here
                    spawnedTile.data.StarterTileValue(false);
                }

                if (spawnedTile.data.CanSpawnObstacle)
                {
                    bool spawnObstacle = Random.value <= obstacleChance;
                    if (spawnObstacle)
                    {
                        // handle spawn obstacle
                        // Debug.Log("Spawned obstacle on " + spawnedTile.gameObject.name);
                        // spawnedTile.transform.Translate(new Vector3(0, 0.5f, 0));
                        // spawnedTile.gameObject.GetComponent<MeshRenderer>().material.color = Color.red;

                        spawnedTile.data.SetOccupied(Tile.ObstacleType.Resource);
                        SpawnObstacle(spawnedTile.transform.position.x, spawnedTile.transform.position.z);
                    }
                }

            }
        }
    }

    /// <summary>
    /// Spawn and return a tile object
    /// </summary>
    /// <param name="xPos">X position inside the world</param>
    /// <param name="zPos">Z position inside the world</param>
    /// <returns></returns>
    TileObject SpawnTile(float xPos, float zPos)
    {
        GameObject tmpTile = Instantiate(tilePrefab);
        tmpTile.transform.position = new Vector3(xPos, 0, zPos);
        tmpTile.name = $"Tile (clone) {xPos}:{zPos}";
        tmpTile.transform.SetParent(tilesHolder);

        // check if the tile is able 

        return tmpTile.GetComponent<TileObject>();
    }

    /// <summary>
    /// Will spawn a resource obstacle in directly in the coordinates
    /// </summary>
    /// <param name="xPos">X Position of the obstacle</param>
    /// <param name="zPos">Z Position of the obstacle</param>
    public void SpawnObstacle(float xPos, float zPos)
    {
        // It has 50% of spawning a wood obstacle
        bool isWood = Random.value <= 0.5f;

        GameObject spawnedObstacle = null;

        if (isWood)
        {
            spawnedObstacle = Instantiate(woodPrefab);
            spawnedObstacle.name = $"{woodPrefab.name} (clone) {xPos}:{zPos}";
        }
        else
        {
            spawnedObstacle = Instantiate(rockPrefab);
            spawnedObstacle.name = $"{rockPrefab.name} (clone) {xPos}:{zPos}";
        }

        spawnedObstacle.transform.position = new Vector3(xPos, tileEndHeight, zPos);
        spawnedObstacle.transform.SetParent(resourcesHolder);
    }
}
