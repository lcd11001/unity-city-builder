using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiRoadHelper : MonoBehaviour
    {
        [SerializeField]
        protected List<AiRoadMarker> pedestrianMarkers;

        [SerializeField]
        protected bool isCorner;

        [SerializeField]
        protected bool hasCrosswalk;

        float approximateThresholdCorner = 0.31f;

        public virtual AiRoadMarker GetPositionForPerdestrianToSpawn(Vector3 structurePosition)
        {
            return GetClosetMarkerTo(structurePosition, pedestrianMarkers);
        }

        private AiRoadMarker GetClosetMarkerTo(Vector3 structurePosition, List<AiRoadMarker> pedestrianMarkers)
        {
            if (isCorner)
            {
                foreach (var marker in pedestrianMarkers)
                {
                    var direction = marker.Position - structurePosition;
                    if (Mathf.Abs(direction.x) < approximateThresholdCorner || Mathf.Abs(direction.z) < approximateThresholdCorner)
                    {
                        return marker;
                    }
                }
            }
            else
            {
                AiRoadMarker closetMarker = null;
                float distance = float.MaxValue;
                foreach (var marker in pedestrianMarkers)
                {
                    var markerDistance = Vector3.Distance(structurePosition, marker.Position);
                    if (distance > markerDistance)
                    {
                        distance = markerDistance;
                        closetMarker = marker;
                    }
                }

                Debug.Assert(closetMarker != null, $"1. can not get closet marker from structure {structurePosition} with {string.Join(",", pedestrianMarkers.Select(x => x.Position))}");
                return closetMarker;
            }

            Debug.Assert(false, $"2. can not get closet marker from structure {structurePosition} with {string.Join(",", pedestrianMarkers.Select(x => x.Position))}");
            return null;
        }
        public Vector3 GetClosetPedestrianPosition(Vector3 currentPosition)
        {
            return GetClosetMarkerTo(currentPosition, pedestrianMarkers).Position;
        }

        public List<AiRoadMarker> GetAllPedestrianMarker()
        {
            return pedestrianMarkers;
        }
    }

}
