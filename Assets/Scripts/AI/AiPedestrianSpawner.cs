using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AiPedestrianSpawner : MonoBehaviour
{
    public GameObject[] pedestrianPrefabs;

    private void Awake()
    {
        GameObject pedestrian = SelectPedestrianPrefab();
        if (pedestrian != null)
        {
            Instantiate(pedestrian, transform);
        }
    }

    private GameObject SelectPedestrianPrefab()
    {
        if (pedestrianPrefabs.Length > 0)
        {
            var randomIndex = Random.Range(0, pedestrianPrefabs.Length);
            return pedestrianPrefabs[randomIndex];
        }
        return null;
    }
}
