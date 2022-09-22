using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CityBuilder.AI
{
    public class AiSmartCrosswalks : MonoBehaviour
    {
        List<AiAgent> pedestrianList = new List<AiAgent>();

        [field: SerializeField]
        public UnityEvent OnPedestrianEnter { get; set; }

        [field: SerializeField]
        public UnityEvent OnPedestrianExit { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            var pedestrian = other.GetComponent<AiAgent>();
            if (pedestrian != null)
            {
                AddPedestrian(pedestrian);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            var pedestrian = other.GetComponent<AiAgent>();
            if (pedestrian != null)
            {
                RemovePedestrian(pedestrian);
            }
        }

        private void AddPedestrian(AiAgent pedestrian)
        {
            if (pedestrianList.Contains(pedestrian) == false)
            {
                pedestrianList.Add(pedestrian);
                pedestrian.Stop = true;
                OnPedestrianEnter?.Invoke();
            }
        }

        private void RemovePedestrian(AiAgent pedestrian)
        {
            pedestrianList.Remove(pedestrian);
            if (pedestrianList.Count <= 0)
            {
                OnPedestrianExit?.Invoke();
            }
        }

        public void MovePedestrians()
        {
            foreach (var pedestrian in pedestrianList)
            {
                pedestrian.Stop = false;
            }
        }
    }
}
