using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiRoadMarker : MonoBehaviour
    {
        public Vector3 Position { get => transform.position; }

        public List<AiRoadMarker> adjacentMarkers;

        [SerializeField]
        private bool openForConnection;
        public bool OpenForConnection { get => openForConnection; }

        public List<Vector3> GetAdjacentPositions()
        {
            return new List<Vector3>(adjacentMarkers.Select(x => x.Position).ToList());
        }

        private void OnDrawGizmos()
        {
            if (adjacentMarkers == null)
            {
                return;
            }

            Gizmos.color = Color.yellow;
            foreach (var marker in adjacentMarkers)
            {
                Gizmos.DrawLine(Position, marker.Position);
            }
        }
    }
}
