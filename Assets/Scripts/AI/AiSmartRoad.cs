using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.AI
{
    public class AiSmartRoad : MonoBehaviour
    {
        Queue<AiCar> trafficQueue = new Queue<AiCar>();
        public AiCar currentCar;

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
                if (trafficQueue.Count > 0)
                {
                    currentCar = trafficQueue.Dequeue();
                    currentCar.Stop = false;
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
    }
}
