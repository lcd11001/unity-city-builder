using CityBuilder.AI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiRoadCarMarkerCurve : AiRoadCarMarker
    {
        public override AiRoadMarker GetCarIncomingMarker(Vector3 previousPathPosition)
        {
            int angle = (int)transform.rotation.eulerAngles.y;
            var direction = transform.position - previousPathPosition;

            return GetCorrectMarker(angle, direction);
        }

        public override AiRoadMarker GetCarOutgoingMarker(Vector3 nextPathPosition)
        {
            int angle = (int)transform.rotation.eulerAngles.y;
            var direction = nextPathPosition - transform.position;

            return GetCorrectMarker(angle, direction);
        }

        private AiRoadMarker GetCorrectMarker(int angle, Vector3 directionVector)
        {
            var direction = GetDirection(directionVector);
            if (angle == 0)
            {
                if (direction == Direction.left)
                {
                    return incomming;
                }
                else
                {
                    return outgoing;
                }
            }
            else if (angle == 90)
            {
                if (direction == Direction.up)
                {
                    return incomming;
                }
                else
                {
                    return outgoing;
                }
            }
            else if (angle == 180)
            {
                if (direction == Direction.left)
                {
                    return outgoing;
                }
                else
                {
                    return incomming;
                }
            }
            else
            {
                if (direction == Direction.up)
                {
                    return outgoing;
                }
                else
                {
                    return incomming;
                }
            }
        }

        
    }
}
