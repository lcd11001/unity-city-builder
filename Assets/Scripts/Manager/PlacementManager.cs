using System;
using System.Collections;
using System.Collections.Generic;
using CityBuilder.SO;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    [SerializeField] SO_MapConfig mapConfig;
    Grid placementGrid;

    private Dictionary<Vector3Int, StructureModel> tempStructureObjects = new Dictionary<Vector3Int, StructureModel>();

    private void Start()
    {
        placementGrid = new Grid(mapConfig.width, mapConfig.height);
    }

    public CellType[] GetNeighbourTypesFor(Vector3Int position)
    {
        return placementGrid.GetAllAdjacentCellTypes(position.x, position.y);
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
    }
}
