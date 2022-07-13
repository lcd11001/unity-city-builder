using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    Coroutine buildingBehaviour;

    private void Start()
    {
        buildingBehaviour = StartCoroutine("CreateResource");
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

            yield return null;
        }
    }
}
