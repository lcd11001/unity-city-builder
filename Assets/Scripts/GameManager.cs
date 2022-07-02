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
    public int levelWidth;
    public int levelLength;

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
                SpawnTile(x * tileSize, z * tileSize);
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
        return tmpTile.GetComponent<TileObject>();
    }
}
