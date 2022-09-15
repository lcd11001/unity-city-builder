using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiRoadMarker : MonoBehaviour
    {
        public Vector3 Position { get => transform.position; }

        public List<AiRoadMarker> adjacentMarkers = new List<AiRoadMarker>();

        private HashSet<AiRoadMarker> connectedMarkers = new HashSet<AiRoadMarker>();

        [SerializeField]
        private bool openForConnection;
        public bool OpenForConnection { get => openForConnection; }

        public List<Vector3> GetAdjacentPositions()
        {
            return new List<Vector3>(adjacentMarkers.Select(x => x.Position).ToList());
        }

        public void ConnectToMarker(AiRoadMarker other)
        {
            connectedMarkers.Add(other);
        }

        private void OnDrawGizmos()
        {
            if (Selection.activeObject == gameObject)
            {
                if (adjacentMarkers.Count > 0)
                {
                    Gizmos.color = Color.yellow;
                    foreach (var marker in adjacentMarkers)
                    {
                        Gizmos.DrawLine(Position, marker.Position);
                    }
                }

                if (connectedMarkers.Count > 0)
                {
                    Gizmos.color = Color.green;
                    foreach (var marker in connectedMarkers)
                    {
                        Gizmos.DrawLine(Position, marker.Position);
                    }
                }

                Gizmos.color = Color.white;
            }
        }
    }
}
