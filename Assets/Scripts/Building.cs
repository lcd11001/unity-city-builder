using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Building
{
    public int buildingID;

    // X-axis that will be used inside the grid
    public int width = 0;
    // Z-axis that will be used inside the grid
    public int length = 0;

    // type of functionality of the building
    public ResourceType resourceType = ResourceType.None;

    public enum ResourceType
    {
        None,
        Standard,
        Premium
    }
}
