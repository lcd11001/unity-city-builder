using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingObject : MonoBehaviour
{
    public Building data;

    [Header("UI")]
    [Space(8)]
    public Canvas canvasUI;
    public Slider progressSlider;
    public TextMeshProUGUI resourceText;

    Coroutine buildingBehaviour;

    private void Start()
    {
        if (data.resourceType != Building.ResourceType.None && data.resourceType != Building.ResourceType.Storage)
        {
            buildingBehaviour = StartCoroutine("CreateResource");

            resourceText.text = data.resourceType.ToString();
        }

        if (data.resourceType == Building.ResourceType.Storage)
        {
            canvasUI.gameObject.SetActive(false);
            IncreaseMaxStorage();
        }
    }

    private void OnDestroy()
    {
        if (buildingBehaviour != null)
        {
            StopCoroutine(buildingBehaviour);
        }
    }

    private void OnMouseDown()
    {
        bool usedResource = false;

        switch (data.resourceType)
        {
            case Building.ResourceType.Standard:
                usedResource = ResourceManager.Instance.AddStandardC((int)data.resource);
                break;
            case Building.ResourceType.Premium:
                usedResource = ResourceManager.Instance.AddPremiumC((int)data.resource);
                break;
        }

        if (usedResource)
        {
            EmptyResource();
        }
    }

    void EmptyResource()
    {
        data.resource = 0;
    }

    void IncreaseMaxStorage()
    {
        switch (data.storageType)
        {
            case Building.StorageType.Wood:
                ResourceManager.Instance.IncreaseMaxWood((int)data.resourceLimit);
                break;
            case Building.StorageType.Stone:
                ResourceManager.Instance.IncreaseMaxStone((int)data.resourceLimit);
                break;
        }
    }

    IEnumerator CreateResource()
    {
        // It will create resource infinitely
        while (true)
        {
            if (data.resource < data.resourceLimit)
            {
                data.resource += data.generationSpeed * Time.deltaTime;
            }
            else
            {
                data.resource = data.resourceLimit;
            }

            UpdateUI(data.resource, data.resourceLimit);

            yield return null;
        }
    }

    public void UpdateUI(float value, float max)
    {
        progressSlider.value = value;
        progressSlider.maxValue = max;
    }
}
