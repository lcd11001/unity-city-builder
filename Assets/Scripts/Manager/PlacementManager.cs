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
        // GameObject newStructure = Instantiate(structurePrefab, position, Quaternion.identity);
        StructureModel structure = CreateNewStructureModel(position, structurePrefab, type);
        tempStructureObjects.Add(position, structure);
    }

    public List<Vector3Int> GetNeighbourOfTypeFor(Vector3Int position, CellType type)
    {
        var result = placementGrid.GetAdjacentCellsOfType(position.x, position.z, type);
        // convert a list of objects from one type to another using lambda expression 
        return result.Select(point => new Vector3Int(point.X, 0, point.Y)).ToList();
    }

    private StructureModel CreateNewStructureModel(Vector3Int position, GameObject structurePrefab, CellType type)
    {
        GameObject struture = new GameObject(type.ToString());
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
        }
        tempStructureObjects.Clear();
    }
}
