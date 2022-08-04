using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SVS;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public StructurePrefabWeighted[] housesPrefab, specialsPrefab;
    public PlacementManager placementManager;

    private float[] housesWeight, specialsWeight;

    private void Start()
    {
        housesWeight = housesPrefab.Select(prefab => prefab.weight).ToArray();
        specialsWeight = specialsPrefab.Select(prefab => prefab.weight).ToArray();
    }

    public void PlaceHouse(Vector3Int position)
    {
        if (placementManager.CheckPositionBeforePlacement(position, CellType.Road) == false)
        {
            return;
        }

        int randomIndex = GetRandomWeightIndex(housesWeight);
        placementManager.PlaceObjectOnTheMap(position, housesPrefab[randomIndex].prefab, CellType.Structure);
        AudioPlayer.instance.PlayPlacementSound();


    }

    private int GetRandomWeightIndex(float[] weights)
    {
        float sum = weights.Sum();
        float randomValue = UnityEngine.Random.Range(0, sum);
        float tempSum = 0;
        for (int i = 0; i < weights.Length; i++)
        {
            // 0 -> weights[0]
            // weights[0] -> weights[0] + weights[1]
            if (tempSum <= randomValue && randomValue < tempSum + weights[i])
            {
                return i;
            }
            tempSum += weights[i];
        }
        return 0;
    }
}

[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    [Range(0, 1)]
    public float weight;
}
