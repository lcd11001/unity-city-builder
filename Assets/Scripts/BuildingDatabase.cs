using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingDatabase : MonoBehaviour
{
    public List<BuildingObject> buildingsDatabase = new List<BuildingObject>();

    public static BuildingDatabase Instance;

    private void Awake()
    {
        Instance = this;
    }
}
