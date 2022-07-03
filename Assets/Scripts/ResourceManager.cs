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

    public void AddWood(int amount)
    {
        wood = Mathf.Min(wood + amount, maxWood);

        // TODO: update UI 
    }

    public void AddStone(int amount)
    {
        stone = Mathf.Min(stone + amount, maxStone);

        // TODO: update UI 
    }

    public void AddStandardC(int amount)
    {
        standardC = Mathf.Min(standardC + amount, maxStandardCurrency);

        // TODO: update UI 
    }

    public void AddPremiumC(int amount)
    {
        premiumC = Mathf.Min(premiumC + amount, maxPremiumCurrency);

        // TODO: update UI 
    }

    private void DebugValue()
    {
        Debug.Log($"wood: {wood}");
        Debug.Log($"stone: {stone}");
        Debug.Log($"standardC: {standardC}");
        Debug.Log($"premiumC: {premiumC}");
    }
}
