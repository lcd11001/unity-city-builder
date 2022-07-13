using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building
{
    [Header("Building Settings")]
    [Space(8)]
    public int buildingID;

    // X-axis that will be used inside the grid
    public int width = 0;
    // Z-axis that will be used inside the grid
    public int length = 0;

    // Visual of building
    public GameObject buildingModel;

    // type of functionality of the building
    public ResourceType resourceType = ResourceType.None;
    public StorageType storageType = StorageType.None;

    [Header("Resource generation")]
    [Space(8)]
    // this will be the resource that has been created by this building
    public float resource = 0;

    // limit that this building can generate or do
    public float resourceLimit = 100;

    // speed that the resource is generated
    public float generationSpeed = 5;

    public enum ResourceType
    {
        None,
        Standard,
        Premium,
        Storage
    }

    public enum StorageType
    {
        None,
        Wood,
        Stone
    }

    public void Clone(ref Building other)
    {
        other.buildingID = buildingID;
        other.width = width;
        other.length = length;
        other.resourceType = resourceType;
        other.storageType = storageType;
        other.resource = resource;
        other.resourceLimit = resourceLimit;
        other.generationSpeed = generationSpeed;
    }

    public void Copy(in Building other)
    {
        buildingID = other.buildingID;
        width = other.width;
        length = other.length;
        resourceType = other.resourceType;
        storageType = other.storageType;
        resource = other.resource;
        resourceLimit = other.resourceLimit;
        generationSpeed = other.generationSpeed;
    }

}
