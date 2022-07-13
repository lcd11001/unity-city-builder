using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [Space(8)]
    public StandardUI woodUI;
    public StandardUI stoneUI;
    public StandardUI standardCUI;
    public StandardUI premiumCUI;

    public static UIManager Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        woodUI.maxValue.text = @"Max: {ResourceManager.Instance.maxWood}";
    }

    public void UpdateWoodUI(int current, int max)
    {
        woodUI.UpdateUI(current, max);
    }

    public void UpdateStoneUI(int current, int max)
    {
        stoneUI.UpdateUI(current, max);
    }

    public void UpdateStandardCUI(int current, int max)
    {
        standardCUI.UpdateUI(current, max);
    }

    public void UpdatePremiumCUI(int current, int max)
    {
        premiumCUI.UpdateUI(current, max);
    }
}

[System.Serializable]
public class StandardUI
{
    public Slider slider;
    public TextMeshProUGUI maxValue;
    public TextMeshProUGUI currentValue;

    public void UpdateUI(int current, int max)
    {
        maxValue.text = $"Max: {max}";
        currentValue.text = $"{current}";
        
        slider.maxValue = max;
        slider.value = current;
    }
}
