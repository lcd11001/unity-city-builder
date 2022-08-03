using System;
using System.Collections;
using System.Collections.Generic;
using CityBuilder.SO;
using UnityEngine;

namespace CityBuilder.Map
{
    public class Terrain : MonoBehaviour
    {
        [SerializeField] SO_MapConfig mapConfig;

        private void OnValidate()
        {
            ConfigTerrain();
        }

        private void ConfigTerrain()
        {
            float x = (mapConfig.width - 1) * 0.5f;
            float z = (mapConfig.height - 1) * 0.5f;
            Vector3 newPosition = new Vector3(x, 0, z);
            transform.localPosition = newPosition;
            Debug.Log($"Config {gameObject.name} to new position {newPosition}");
        }
    }
}
