using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    [Header("Resources")]
    [Space(8)]
    public int maxWood;
    int wood = 0;

    public int maxStone;
    int stone = 0;

    public int maxPremiumCurrency;
    int premiumC = 0;

    public int maxStandardCurrency;
    int standardC = 0;

    public static ResourceManager Instance;
    public bool debugValue = false;
    private void Awake()
    {
        if (Instance != null)
        {
            Object.Destroy(Instance);
        }
        Instance = this;
    }

    private void Update()
    {
        if (debugValue)
        {
            debugValue = false;
            DebugValue();
        }
    }

    public bool AddWood(int amount)
    {
        int tmp = wood + amount;
        if (tmp > maxWood)
        {
            return false;
        }
        wood = tmp;

        // TODO: update UI 
        return true;
    }

    public bool AddStone(int amount)
    {
        int tmp = stone + amount;
        if (tmp > maxStone)
        {
            return false;
        }
        stone = tmp;

        // TODO: update UI 
        return true;
    }

    public bool AddStandardC(int amount)
    {
        int tmp = standardC + amount;
        if (tmp > maxStandardCurrency)
        {
            return false;
        }
        standardC = tmp;

        // TODO: update UI 
        return true;
    }

    public bool AddPremiumC(int amount)
    {
        int tmp = premiumC + amount;
        if (tmp > maxPremiumCurrency)
        {
            return false;
        }
        premiumC = tmp;

        // TODO: update UI 
        return true;
    }

    private void DebugValue()
    {
        Debug.Log($"wood: {wood}");
        Debug.Log($"stone: {stone}");
        Debug.Log($"standardC: {standardC}");
        Debug.Log($"premiumC: {premiumC}");
    }
}
