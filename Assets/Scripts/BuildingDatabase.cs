using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDatabase : MonoBehaviour
{
    public List<Building> buildingsDatabase = new List<Building>();

    public static BuildingDatabase Instance;

    private void Awake()
    {
        Instance = this;
    }
}
