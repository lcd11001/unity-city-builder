using CityBuilder.AI;
using System.Collections;
using UnityEngine;

public interface ICarMarker
{
    AiRoadMarker GetCarIncomingMarker(Vector3 previousPathPosition);
    AiRoadMarker GetCarOutgoingMarker(Vector3 nextPathPosition);
}
