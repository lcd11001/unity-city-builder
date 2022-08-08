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

    public Vector3 GetNearestMarkerTo(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<AiRoadHelper>().GetClosetPedestrianPosition(position);
    }

    public AiRoadMarker GetPedestrianSpawnMarker(Vector3 position)
    {
        return transform.GetChild(0).GetComponent<AiRoadHelper>().GetPositionForPerdestrianToSpawn(position);
    }

    public List<AiRoadMarker> GetPedestrianMarkers()
    {
        return transform.GetChild(0).GetComponent<AiRoadHelper>().GetAllPedestrianMarker();
    }
}
