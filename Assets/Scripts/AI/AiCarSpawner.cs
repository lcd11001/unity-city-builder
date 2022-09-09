using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AiCarSpawner : MonoBehaviour
{
    public GameObject[] carPrefabs;

    private void Start()
    {
        GameObject car = SelectCarPrefab();
        if (car != null)
        {
            Instantiate(car, transform);
        }
    }

    private GameObject SelectCarPrefab()
    {
        if (carPrefabs.Length > 0)
        {
            var randomIndex = Random.Range(0, carPrefabs.Length);
            return carPrefabs[randomIndex];
        }
        return null;
    }
}
