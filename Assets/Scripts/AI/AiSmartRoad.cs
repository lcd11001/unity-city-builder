using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace CityBuilder.AI
{
    public class AiSmartRoad : MonoBehaviour
    {
        Queue<AiCar> trafficQueue = new Queue<AiCar>();
        public AiCar currentCar;

        [SerializeField]
        private bool pedestrianWaiting = false, pedestrianWalking = false;

        [field: SerializeField]
        public UnityEvent  OnPedestrianCanWalk { get; set; }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Car"))
            {
                var car = other.GetComponent<AiCar>();
                if (car != null)
                {
                    AddCar(car);
                }
            }
        }

        private void Update()
        {
            if (currentCar == null)
            {
                if (trafficQueue.Count > 0 && pedestrianWaiting == false && pedestrianWalking == false)
                {
                    currentCar = trafficQueue.Dequeue();
                    currentCar.Stop = false;
                }
                else if (pedestrianWalking || pedestrianWaiting)
                {
                    OnPedestrianCanWalk?.Invoke();
                    pedestrianWalking = true;
                }

            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Car"))
            {
                var car = other.GetComponent<AiCar>();
                if (car != null)
                {
                    RemoveCar(car);
                }
            }
        }

        private void RemoveCar(AiCar car)
        {
            if (car == currentCar)
            {
                currentCar = null;
            }
        }

        private void AddCar(AiCar car)
        {
            if (car != currentCar && car.IsThisLastPathIndex() == false)
            {
                trafficQueue.Enqueue(car);
                car.Stop = true;
            }
        }

        public void SetPedestrianFlag(bool val)
        {
            if (val)
            {
                pedestrianWaiting = true;
            }
            else
            {
                pedestrianWaiting = false;
                pedestrianWalking = false;
            }
        }
    }
}
