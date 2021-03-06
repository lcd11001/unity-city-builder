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

    [Space(8)]
    public TileObject[,] tileGrid = new TileObject[0, 0];

    [Header("Resources")]
    [Space(8)]
    public Transform resourcesHolder;
    public GameObject woodPrefab;
    public GameObject rockPrefab;
    [Range(0, 1)]
    public float obstacleChance = 0.3f;

    public int xBounds = 3;
    public int zBounds = 3;

    [Header("Buildings")]
    [Space(8)]
    // Debug method (the selected building)
    public Transform buildingsHolder;
    public BuildingObject buildingToPlace;


    public static GameManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        CreateLevel();
    }

    /// <summary>
    /// Create out grid depending on our level width and length
    /// </summary>
    public void CreateLevel()
    {
        CreateGrid(levelWidth, levelLength);

        for (int x = 0; x < levelWidth; x++)
        {
            for (int z = 0; z < levelLength; z++)
            {
                // Directly spawn a tile
                TileObject spawnedTile = SpawnTile(x * tileSize, z * tileSize);

                // Set the TileObject world space data
                spawnedTile.xPos = x;
                spawnedTile.zPos = z;

                // Check whenever we can spawn an obstacle inside a tile, using bound data
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
                        spawnedTile.data.SetOccupied(Tile.ObstacleType.Resource);
                        ObstacleObject spawnedObstacle = SpawnObstacle(spawnedTile.transform.position.x, spawnedTile.transform.position.z);
                        spawnedObstacle.SetTileReference(spawnedTile);
                    }
                }

                // Add the spawned visual tile object inside the grid
                UpdateGrid(x, z, spawnedTile);
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
        tmpTile.name = $"{tilePrefab.name} (clone) {xPos}:{zPos}";
        tmpTile.transform.SetParent(tilesHolder);

        return tmpTile.GetComponent<TileObject>();
    }

    /// <summary>
    /// Will spawn a resource obstacle in directly in the coordinates
    /// </summary>
    /// <param name="xPos">X Position of the obstacle</param>
    /// <param name="zPos">Z Position of the obstacle</param>
    ObstacleObject SpawnObstacle(float xPos, float zPos)
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

        ObstacleObject obstacle = spawnedObstacle.GetComponent<ObstacleObject>();
        obstacle.data.obstacleType = isWood ? Obstacle.ObstacleType.Wood : Obstacle.ObstacleType.Rock;
        return obstacle;
    }

    /// <summary>
    /// Create tile grid to add buildings
    /// </summary>
    /// <param name="width">width of grid</param>
    /// <param name="length">length of grid</param>
    public void CreateGrid(int width, int length)
    {
        tileGrid = new TileObject[levelWidth, levelLength];
    }

    /// <summary>
    /// Update tile grid data
    /// </summary>
    /// <param name="xPos">X position in real world</param>
    /// <param name="zPos">Z position in real world</param>
    /// <param name="tile">tile data</param>
    public void UpdateGrid(int xPos, int zPos, TileObject tile)
    {
        tileGrid[xPos, zPos] = tile;
        // Debug.Log(tileGrid[xPos, zPos].gameObject.name);
    }

    public void UpdateGridBuilding(int xPos, int zPos, BuildingObject building)
    {
        for (int x = xPos; x < xPos + building.data.width; x++)
        {
            for (int z = zPos; z < zPos + building.data.length; z++)
            {
                tileGrid[x, z].data.SetOccupied(Tile.ObstacleType.Building, building);
            }
        }
    }

    /// <summary>
    /// Placing of the building
    /// </summary>
    /// <param name="building">building to place</param>
    /// <param name="tile">tile to place the buliding to</param>
    public BuildingObject SpawnBuilding(BuildingObject building, TileObject tile)
    {
        GameObject spawnBuilding = Instantiate(building.gameObject);
        spawnBuilding.transform.SetParent(buildingsHolder);
        spawnBuilding.name = $"{building.name} (clone) {tile.xPos}:{tile.zPos}";

        Vector3 position = new Vector3(tile.xPos * tileSize, tileEndHeight, tile.zPos * tileSize);
        spawnBuilding.transform.position = position;

        return spawnBuilding.GetComponent<BuildingObject>();
    }

    public void SelectBuilding(int id)
    {
        Building building = BuildingDatabase.Instance.buildingsDatabase.Find(building => building.buildingID == id);
        if (building != null)
        {
            buildingToPlace = building.buildingModel.GetComponent<BuildingObject>();
            
            // fixed: buildingToPlace not correct as settings in database
            buildingToPlace.data.Copy(building);
        }
    }
}
