using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BuildingObject : MonoBehaviour
{
    public Building data;

    [Header("Resource generation")]
    [Space(8)]
    // this will be the resource that has been created by this building
    public float resource = 0;

    // limit that this building can generate or do
    public float resourceLimit = 100;

    // speed that the resource is generated
    public float generationSpeed = 5;

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
        StopCoroutine(buildingBehaviour);
    }

    private void OnMouseDown()
    {
        bool usedResource = false;

        switch (data.resourceType)
        {
            case Building.ResourceType.Standard:
                usedResource = ResourceManager.Instance.AddStandardC((int)resource);
                break;
            case Building.ResourceType.Premium:
                usedResource = ResourceManager.Instance.AddPremiumC((int)resource);
                break;
        }

        if (usedResource)
        {
            EmptyResource();
        }
    }

    void EmptyResource()
    {
        resource = 0;
    }

    void IncreaseMaxStorage()
    {
        switch (data.storageType)
        {
            case Building.StorageType.Wood:
                ResourceManager.Instance.IncreaseMaxWood((int)resourceLimit);
                break;
            case Building.StorageType.Stone:
                ResourceManager.Instance.IncreaseMaxStone((int)resourceLimit);
                break;
        }
    }

    IEnumerator CreateResource()
    {
        // It will create resource infinitely
        while (true)
        {
            if (resource < resourceLimit)
            {
                resource += generationSpeed * Time.deltaTime;
            }
            else
            {
                resource = resourceLimit;
            }

            UpdateUI(resource, resourceLimit);

            yield return null;
        }
    }

    public void UpdateUI(float value, float max)
    {
        progressSlider.value = value;
        progressSlider.maxValue = max;
    }
}
