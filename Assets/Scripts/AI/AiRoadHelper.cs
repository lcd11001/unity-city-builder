using System.Collections;
using System.Collections.Generic;
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
    }
}
