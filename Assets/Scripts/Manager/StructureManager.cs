using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SVS;
using UnityEngine;

public class StructureManager : MonoBehaviour
{
    public StructurePrefabWeighted[] housesPrefab, specialsPrefab, bigStructuresPrefab;
    public PlacementManager placementManager;

    private float[] housesWeight, specialsWeight, bigStructuresWeight;

    private void Start()
    {
        housesWeight = housesPrefab.Select(prefab => prefab.weight).ToArray();
        specialsWeight = specialsPrefab.Select(prefab => prefab.weight).ToArray();
        bigStructuresWeight = bigStructuresPrefab.Select(prefab => prefab.weight).ToArray();
    }

    public void PlaceHouse(Vector3Int position)
    {
        int randomIndex = GetRandomWeightIndex(housesWeight);
        PlaceAnyObject(position, housesPrefab[randomIndex], CellType.Structure);
    }

    public void PlaceSpecial(Vector3Int position)
    {
        int randomIndex = GetRandomWeightIndex(specialsWeight);
        PlaceAnyObject(position, specialsPrefab[randomIndex], CellType.SpecialStructure);
    }

    public void PlaceBigStructure(Vector3Int position)
    {
        int randomIndex = GetRandomWeightIndex(bigStructuresWeight);
        PlaceAnyObject(position, bigStructuresPrefab[randomIndex], CellType.BigStructure);
    }

    private void PlaceAnyObject(Vector3Int position, StructurePrefabWeighted structure, CellType type)
    {
        if (placementManager.CheckPositionBeforePlacementBigObject(position, CellType.Road, structure.width, structure.height) == false)
        {
            AudioPlayer.instance.PlayPlacementError();
            return;
        }
        placementManager.PlaceBigObjectOnTheMap(position, structure.prefab, structure.width, structure.height, type);
        AudioPlayer.instance.PlayPlacementSound();
    }

    private int GetRandomWeightIndex(float[] weights)
    {
        float sum = weights.Sum();
        float randomValue = UnityEngine.Random.Range(0, sum);
        float tempSum = 0;
        float nearestValue = sum;
        int nearestIndex = 0;

        for (int i = 0; i < weights.Length; i++)
        {
            // 0 -> weights[0]
            // weights[0] -> weights[0] + weights[1]
            if (tempSum <= randomValue && randomValue < tempSum + weights[i])
            {
                // found randomIndex;
                return i;
            }
            if (randomValue - weights[i] < nearestValue)
            {
                nearestValue = randomValue - weights[i];
                nearestIndex = i;
            }
            tempSum += weights[i];
        }

        //  incase we didn't find randomIndex;
        return nearestIndex;
    }
}

[Serializable]
public struct StructurePrefabWeighted
{
    public GameObject prefab;
    [Range(0, 1)]
    public float weight;
    [Min(1)]
    public int width;
    [Min(1)]
    public int height;
}
