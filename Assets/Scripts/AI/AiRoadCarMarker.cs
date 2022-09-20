using System.Collections;
using UnityEngine;

namespace CityBuilder.AI
{
    [RequireComponent(typeof(AiRoadHelper))]
    public abstract class AiRoadCarMarker : MonoBehaviour, ICarMarker
    {
        [SerializeField]
        protected AiRoadMarker incomming, outgoing;

        protected AiRoadHelper roadHelper;

        private void Start()
        {
            roadHelper = GetComponent<AiRoadHelper>();
        }

        public enum Direction
        {
            up,
            down,
            left,
            right
        }

        public Direction GetDirection(Vector3 direction)
        {
            if (Mathf.Abs(direction.z) > .5f)
            {
                if (direction.z > 0.5f)
                {
                    return Direction.up;
                }
                else
                {
                    return Direction.down;
                }
            }
            else
            {
                if (direction.x > 0.5f)
                {
                    return Direction.right;
                }
                else
                {
                    return Direction.left;
                }
            }
        }

        public abstract AiRoadMarker GetCarIncomingMarker(Vector3 previousPathPosition);
        public abstract AiRoadMarker GetCarOutgoingMarker(Vector3 nextPathPosition);
        
    }
}