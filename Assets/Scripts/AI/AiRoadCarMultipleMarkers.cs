using CityBuilder.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiRoadCarMultipleMarkers : AiRoadCarMarker
    {
        [SerializeField]
        protected List<AiRoadMarker> incommingMarkers, outgoingMarkers;
        public override AiRoadMarker GetCarIncomingMarker(Vector3 previousPathPosition)
        {
            return roadHelper.GetClosetMarkerTo(previousPathPosition, incommingMarkers);
        }

        public override AiRoadMarker GetCarOutgoingMarker(Vector3 nextPathPosition)
        {
            return roadHelper.GetClosetMarkerTo(nextPathPosition, outgoingMarkers);
        }
    }
}
