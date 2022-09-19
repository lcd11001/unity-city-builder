using System;
using System.Collections;
using System.Collections.Generic;
using CityBuilder.AI;
using UnityEngine;

public class StructureModel : MonoBehaviour, INeedingRoad
{
    float yHeight = 0;

    public Vector3Int RoadPosition { get; set; }

    public void CreateModel(GameObject model)
    {
        var structure = Instantiate(model, transform);
        yHeight = structure.transform.position.y;
    }

    public void SwapModel(GameObject model, Quaternion rotation)
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        var structure = Instantiate(model, transform);
        structure.transform.localPosition = new Vector3(0, yHeight, 0);
        structure.transform.localRotation = rotation;
    }

    public AiRoadMarker GetNearestPedestrianMarkerTo(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<AiRoadHelper>().GetNearestPedestrianMarker(position);
    }

    public AiRoadMarker GetPedestrianSpawnMarker(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<AiRoadHelper>().GetPerdestrianSpawnMarker(position);
    }

    public List<AiRoadMarker> GetPedestrianMarkers()
    {
        return transform.GetChild(0).GetComponent<AiRoadHelper>().GetAllPedestrianMarker();
    }

    public AiRoadMarker GetNearestCarMarkerTo(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<AiRoadHelper>().GetNearestCarMarker(position);
    }

    public AiRoadMarker GetCarOutgoingMarker(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<AiRoadHelper>().GetCarOutgoingMarker(position);
    }

    public AiRoadMarker GetCarIncomingMarker(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<AiRoadHelper>().GetCarIncomingMarker(position);
    }

    public List<AiRoadMarker> GetCarMarkers()
    {
        return transform.GetChild(0).GetComponent<AiRoadHelper>().GetAllCarMarker();
    }
}
