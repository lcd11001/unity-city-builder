using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BuildingSystem : MonoBehaviour
{
    public static BuildingSystem current;
    [SerializeField] private Camera mainCamera;

    [SerializeField] private GridLayout gridLayout;
    private Grid grid;

    [SerializeField] private Tilemap MainTilemap;
    [SerializeField] private TileBase whiteTile;
    [SerializeField] private GameObject prefab1;
    [SerializeField] private GameObject prefab2;

    [SerializeField] private GameObject hitSphere;

    private PlayableObject objectToPlace;
    private TintColorObject tintColorObject;
    private bool mouseFilter = false;

    private void Awake()
    {
        current = this;
        grid = gridLayout.gameObject.GetComponent<Grid>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            mouseFilter = !mouseFilter;
        }

        //Debug.Log("-----------------");
        Vector3 mouseWorldPos = GetMouseWorldPosition();
        //Debug.Log("Mouse pos" + mouseWorldPos);
        hitSphere.transform.position = mouseWorldPos;
        if (mouseFilter)
        {
            Vector3Int cellPos = GetWorldToCell(mouseWorldPos);
            //Debug.Log("Cell pos" + cellPos);
            TakeArea(cellPos, Vector3Int.zero);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            InitPlaceObject(prefab1);
        }
        else if (Input.GetKeyDown(KeyCode.B))
        {
            InitPlaceObject(prefab2);
        }

        if (!objectToPlace)
        {
            return;
        }

        bool canBePlace = CanBePlaced(objectToPlace);
        if (!canBePlace)
        {
            tintColorObject.TintColor(new Color(1.0f, 0f, 0f, 0.8f));
        }
        else
        {
            tintColorObject.ResetColor();
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            objectToPlace.Rotate();
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            if (canBePlace)
            {
                objectToPlace.Place();
                Vector3Int start = GetWorldToCell(objectToPlace.GetStartPosition());
                TakeArea(start, objectToPlace.Size);
                Destroy(objectToPlace);
                objectToPlace = null;
            }
            //else
            //{
            //    Destroy(objectToPlace.gameObject);
            //}
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            Destroy(objectToPlace.gameObject);
        }
    }

    public Vector3Int GetWorldToCell(Vector3 pos)
    {
        return gridLayout.WorldToCell(pos);
    }

    public Vector3 SnapCoordinateToGrid(Vector3 pos)
    {
        Vector3Int cellPos = GetWorldToCell(pos);
        Vector3 cellCenterPos = grid.GetCellCenterWorld(cellPos);
        return cellCenterPos;
    }

    public static Vector3 GetMouseWorldPosition()
    {
        Ray ray = BuildingSystem.current.mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit /*, Mathf.Infinity, LayerMask.NameToLayer("Ground")*/))
        {
            return raycastHit.point;
        }
        return Vector3.zero;
    }

    private static TileBase[] GetTilesBlock(BoundsInt area, Tilemap tilemap)
    {
        TileBase[] array = new TileBase[area.size.x * area.size.y * area.size.z];
        int counter = 0;
        foreach (var v in area.allPositionsWithin)
        {
            Vector3Int pos = new Vector3Int(v.x, v.y, 0);
            array[counter] = tilemap.GetTile(pos);
            counter++;
        }

        return array;
    }

    public void InitPlaceObject(GameObject prefab)
    {
        Vector3 pos = SnapCoordinateToGrid(Vector3.zero);
        GameObject obj = Instantiate(prefab, pos, Quaternion.identity, MainTilemap.transform);

        obj.AddComponent<ObjectDrag>();
        objectToPlace = obj.AddComponent<PlayableObject>();
        tintColorObject = obj.AddComponent<TintColorObject>();
    }

    private bool CanBePlaced(PlayableObject playableObject)
    {
        BoundsInt area = new BoundsInt();
        area.position = GetWorldToCell(playableObject.GetStartPosition());
        area.size = playableObject.Size;

        TileBase[] baseArray = BuildingSystem.GetTilesBlock(area, MainTilemap);

        foreach (var b in baseArray)
        {
            if (b == whiteTile)
            {
                return false;
            }
        }

        return true;
    }

    public void TakeArea(Vector3Int start, Vector3Int size)
    {
        //Debug.Log($"TakeArea start {start.ToString()} size {size.ToString()}");
        MainTilemap.BoxFill(start, whiteTile, start.x, start.y, start.x + size.x, start.y + size.y);
    }
}
