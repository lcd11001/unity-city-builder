using CityBuilder.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiRoadCarMarkerDeadEnd :AiRoadCarMarker
    {        
        public override AiRoadMarker GetCarIncomingMarker(Vector3 previousPathPosition)
        {
            return incomming;
        }

        public override AiRoadMarker GetCarOutgoingMarker(Vector3 nextPathPosition)
        {
            return outgoing;
        }
        
    }
}
