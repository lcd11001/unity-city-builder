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
        protected List<AiRoadMarker> carMarkers;

        [SerializeField]
        protected bool isCorner;

        [SerializeField]
        protected bool hasCrosswalk;

        [SerializeField]
        protected AiRoadMarker incomming, outgoing;

        float approximateThresholdCorner = 0.31f;

        public AiRoadMarker GetPerdestrianSpawnMarker(Vector3 structurePosition)
        {
            return GetClosetMarkerTo(structurePosition, pedestrianMarkers);
        }

        public AiRoadMarker GetNearestPedestrianMarker(Vector3 currentPosition)
        {
            return GetClosetMarkerTo(currentPosition, pedestrianMarkers, isCorner);
        }

        private AiRoadMarker GetClosetMarkerTo(Vector3 currentPosition, List<AiRoadMarker> markers, bool isCorner = false)
        {
            if (isCorner)
            {
                foreach (var marker in markers)
                {
                    var direction = marker.Position - currentPosition;
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
                foreach (var marker in markers)
                {
                    var markerDistance = Vector3.Distance(currentPosition, marker.Position);
                    if (distance > markerDistance)
                    {
                        distance = markerDistance;
                        closetMarker = marker;
                    }
                }

                Debug.Assert(closetMarker != null, $"1. can not get closet marker from structure {currentPosition} with {string.Join(",", markers.Select(x => x.Position))}");
                return closetMarker;
            }

            Debug.Assert(false, $"2. can not get closet marker from structure {currentPosition} with {string.Join(",", markers.Select(x => x.Position))}");
            return null;
        }

        public List<AiRoadMarker> GetAllPedestrianMarker()
        {
            return pedestrianMarkers;
        }

        public virtual AiRoadMarker GetCarOutgoingMarker(Vector3 nextPathPosition)
        {
            return outgoing;
        }

        public virtual AiRoadMarker GetCarIncomingMarker(Vector3 previousPathPosition)
        {
            return incomming;
        }

        public AiRoadMarker GetNearestCarMarker(Vector3 currentPosition)
        {
            return GetClosetMarkerTo(currentPosition, carMarkers, false);
        }

        public List<AiRoadMarker> GetAllCarMarker()
        {
            return carMarkers;
        }
    }

}
