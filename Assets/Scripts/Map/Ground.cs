using System.Collections;
using System.Collections.Generic;
using CityBuilder.SO;
using UnityEngine;

namespace CityBuilder.Map
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Ground : MonoBehaviour
    {
        SO_MapConfig mapConfig;

        private void OnValidate()
        {
            mapConfig = FindObjectOfType<MapConfig>().Instance;
            ConfigGround();
        }

        private void ConfigGround()
        {
            ConfigGroundMaterial();
            ConfigGroundTransform();
        }

        private void ConfigGroundTransform()
        {
            float x = mapConfig.width;
            float y = mapConfig.depth;
            float z = mapConfig.height;
            Vector3 newScale = new Vector3(x, y, z);

            // Debug.Log($"Config {gameObject.name} to new scale {newScale}");
            transform.localScale = newScale;

            Vector3 newPosition = new Vector3(0, (y * -0.5f) - 0.01f, 0);
            // Debug.Log($"Config {gameObject.name} to new position {newPosition}");
            transform.localPosition = newPosition;
        }

        private void ConfigGroundMaterial()
        {
            var mr = GetComponent<MeshRenderer>();
            mr.material = mapConfig.groundMaterial;
        }
    }
}
