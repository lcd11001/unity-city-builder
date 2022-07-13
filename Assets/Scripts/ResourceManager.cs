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

    private void Start()
    {
        UIManager.Instance.UpdateWoodUI(wood, maxWood);
        UIManager.Instance.UpdateStoneUI(stone, maxStone);
        UIManager.Instance.UpdateStandardCUI(standardC, maxStandardCurrency);
        UIManager.Instance.UpdatePremiumCUI(premiumC, maxPremiumCurrency);
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
        UIManager.Instance.UpdateWoodUI(wood, maxWood);
        return true;
    }

    public void IncreaseMaxWood(int amount)
    {
        maxWood += amount;
        UIManager.Instance.UpdateWoodUI(wood, maxWood);
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
        UIManager.Instance.UpdateStoneUI(stone, maxStone);
        return true;
    }

    public void IncreaseMaxStone(int amount)
    {
        maxStone += amount;
        UIManager.Instance.UpdateStoneUI(stone, maxStone);
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
        UIManager.Instance.UpdateStandardCUI(standardC, maxStandardCurrency);
        return true;
    }

    public void IncreaseMaxStandardCurrency(int amount)
    {
        maxStandardCurrency += amount;
        UIManager.Instance.UpdateStandardCUI(standardC, maxStandardCurrency);
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
        UIManager.Instance.UpdatePremiumCUI(premiumC, maxPremiumCurrency);
        return true;
    }

    public void IncreaseMaxPremiumCurrency(int amount)
    {
        maxPremiumCurrency += amount;
        UIManager.Instance.UpdatePremiumCUI(premiumC, maxPremiumCurrency);
    }

    private void DebugValue()
    {
        Debug.Log($"wood: {wood}");
        Debug.Log($"stone: {stone}");
        Debug.Log($"standardC: {standardC}");
        Debug.Log($"premiumC: {premiumC}");
    }
}
