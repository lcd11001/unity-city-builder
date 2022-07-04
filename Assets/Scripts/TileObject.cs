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
        Debug.Log("Clicked on " + gameObject.name);
    }
}
