using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CityBuilder.Map;
using CityBuilder.SO;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    SO_MapConfig mapConfig;
    Grid placementGrid;

    private Dictionary<Vector3Int, StructureModel> tempStructureObjects = new Dictionary<Vector3Int, StructureModel>();
    private Dictionary<Vector3Int, StructureModel> structureObjects = new Dictionary<Vector3Int, StructureModel>();

    private void Start()
    {
        mapConfig = FindObjectOfType<MapConfig>().Instance;
        placementGrid = new Grid(mapConfig.width, mapConfig.height);
    }

    public CellType[] GetNeighbourAllTypesFor(Vector3Int position)
    {
        return placementGrid.GetAllAdjacentCellTypes(position.x, position.z);
    }

    public bool CheckPositionBeforePlacementBigObject(Vector3Int position, CellType neighbourType, int width, int height)
    {
        bool nearNeighbour = false;

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                if (DefaultCheck(newPosition) == false)
                {
                    return false;
                }
                if (nearNeighbour == false)
                {
                    nearNeighbour = NeighbourTypeCheck(newPosition, neighbourType);
                }
            }
        }

        return nearNeighbour;
    }

    public bool CheckPositionBeforePlacement(Vector3Int position, CellType neighbourType)
    {
        if (DefaultCheck(position) == false)
        {
            return false;
        }

        if (NeighbourTypeCheck(position, neighbourType) == false)
        {
            return false;
        }

        return true;
    }

    private bool NeighbourTypeCheck(Vector3Int position, CellType neighbourType)
    {
        var neighbours = GetNeighbourOfTypeFor(position, neighbourType);
        if (neighbours.Count <= 0)
        {
            // Debug.Log($"must be placed near a {neighbourType}");
            return false;
        }
        return true;
    }

    private bool DefaultCheck(Vector3Int position)
    {
        if (CheckIfPositionInBound(position) == false)
        {
            // out side the grid, not the Unity map
            // Debug.Log($"this position {position.x}:{position.z} is out of bound");
            return false;
        }

        if (CheckIfPositionIsFree(position) == false)
        {
            // Debug.Log($"this position {position.x}:{position.z} is not empty");
            return false;
        }
        return true;
    }

    public bool CheckIfPositionInBound(Vector3Int position)
    {
        if (position.x >= 0 && position.x < mapConfig.width && position.z >= 0 && position.z < mapConfig.height)
        {
            return true;
        }

        return false;
    }

    public bool CheckIfPositionIsFree(Vector3Int position)
    {
        return CheckIfPositionIsOfType(position, CellType.Empty);
    }

    private bool CheckIfPositionIsOfType(Vector3Int position, CellType type)
    {
        return placementGrid[position.x, position.z] == type;
    }

    public void PlaceTemporaryStructure(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        placementGrid[position.x, position.z] = type;
        StructureModel structure = CreateNewStructureModel(position, structurePrefab, type);
        tempStructureObjects.Add(position, structure);
    }

    public void PlaceObjectOnTheMap(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        placementGrid[position.x, position.z] = type;
        StructureModel structure = CreateNewStructureModel(position, structurePrefab, type);
        structureObjects.Add(position, structure);

        DestroyNatureAt(position);
    }

    public void PlaceBigObjectOnTheMap(Vector3Int position, GameObject structurePrefab, int width, int height, CellType type)
    {
        StructureModel structure = CreateNewStructureModel(position, structurePrefab, type);

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                var newPosition = position + new Vector3Int(x, 0, z);
                placementGrid[newPosition.x, newPosition.z] = type;
                structureObjects.Add(newPosition, structure);

                DestroyNatureAt(newPosition);
            }
        }
    }

    private void DestroyNatureAt(Vector3Int position)
    {
        Vector3 centerBox = position + new Vector3(0, 0.5f, 0);
        Vector3 halfExtents = new Vector3(0.5f, 0.5f, 0.5f);
        Vector3 direction = transform.up;

        RaycastHit[] hits = Physics.BoxCastAll(centerBox, halfExtents, direction, Quaternion.identity, Mathf.Infinity, 1 << LayerMask.NameToLayer("Nature"));

        foreach (var item in hits)
        {
            Destroy(item.collider.gameObject);
        }
    }

    public List<Vector3Int> GetNeighbourOfTypeFor(Vector3Int position, CellType type)
    {
        var result = placementGrid.GetAdjacentCellsOfType(position.x, position.z, type);
        // convert a list of objects from one type to another using lambda expression 
        return result.Select(point => new Vector3Int(point.X, 0, point.Y)).ToList();
    }

    private StructureModel CreateNewStructureModel(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        GameObject struture = new GameObject($"{type.ToString()} {position.x}:{position.z}");
        struture.transform.SetParent(transform);
        struture.transform.localPosition = position;

        var structureModel = struture.AddComponent<StructureModel>();
        structureModel.CreateModel(structurePrefab);

        return structureModel;
    }

    public void ModifyStructureModel(Vector3Int position, GameObject newModel, Quaternion rotation)
    {
        if (tempStructureObjects.ContainsKey(position))
        {
            tempStructureObjects[position].SwapModel(newModel, rotation);
        }
        else if (structureObjects.ContainsKey(position))
        {
            structureObjects[position].SwapModel(newModel, rotation);
        }
    }

    public List<Vector3Int> GetPathBetween(Vector3Int startPosition, Vector3Int endPosition)
    {
        var resultPath = GridSearch.AStarSearch(placementGrid, new Point(startPosition.x, startPosition.z), new Point(endPosition.x, endPosition.z));
        return resultPath.Select(point => new Vector3Int(point.X, 0, point.Y)).ToList();
    }

    public void RemoveAllTemporaryStructures()
    {
        foreach (var obj in tempStructureObjects)
        {
            var position = obj.Key;
            var structure = obj.Value;
            placementGrid[position.x, position.z] = CellType.Empty;
            Destroy(structure.gameObject);
        }
        tempStructureObjects.Clear();
    }

    public void SaveTemporaryStructures()
    {
        foreach (var obj in tempStructureObjects)
        {
            structureObjects.Add(obj.Key, obj.Value);
            DestroyNatureAt(obj.Key);
        }
        tempStructureObjects.Clear();
    }

    private StructureModel GetStructureAt(Point point)
    {
        if (point == null)
        {
            return null;
        }
        return GetStructureAt(new Vector3Int(point.X, 0, point.Y));
    }

    public StructureModel GetStructureAt(Vector3Int position)
    {
        if (structureObjects.ContainsKey(position))
        {
            return structureObjects[position];
        }
        return null;
    }

    public List<StructureModel> GetAllRoad()
    {
        return placementGrid.GetAllRoadFromGrid()
            .Select(point => GetStructureAt(point))
            .Where(structure => structure != null)
            .ToList();
    }

    public List<StructureModel> GetAllSpecialStructure()
    {
        return placementGrid.GetAllSpecialStructuresFromGrid()
            .Select(point => GetStructureAt(point))
            .Where(structure => structure != null)
            .ToList();
    }

    public List<StructureModel> GetAllHouseStructure()
    {
        return placementGrid.GetAllHouseStructuresFromGrid()
            .Select(point => GetStructureAt(point))
            .Where(structure => structure != null)
            .ToList();
    }

    public List<StructureModel> GetAllBigStructure()
    {
        return placementGrid.GetAllBigStructuresFromGrid()
            .Select(point => GetStructureAt(point))
            .Where(structure => structure != null)
            .ToList();
    }

    public StructureModel GetRandomHouseStructure()
    {
        var point = placementGrid.GetRandomHouseStructurePoint();
        return GetStructureAt(point);
    }

    public StructureModel GetRandomSpecialStructure()
    {
        var point = placementGrid.GetRandomSpecialStructurePoint();
        return GetStructureAt(point);
    }

    public StructureModel GetRandomBigStructure()
    {
        var point = placementGrid.GetRandomBigStructurePoint();
        return GetStructureAt(point);
    }

    public StructureModel GetRandomRoad()
    {
        var point = placementGrid.GetRandomRoadPoint();
        return GetStructureAt(point);
    }
}
