using System.Collections;
using System.Collections.Generic;
using CityBuilder.SO;
using UnityEngine;

namespace CityBuilder.Map
{
    [RequireComponent(typeof(MeshRenderer))]
    public class Plane : MonoBehaviour
    {
        [SerializeField] SO_MapConfig mapConfig;

        private void OnValidate()
        {
            ConfigPlane();
        }

        private void ConfigPlane()
        {
            ConfigPlaneMaterial();
            ConfigPlaneTransform();
        }

        private void ConfigPlaneTransform()
        {
            float x = mapConfig.width * 0.1f;
            float z = mapConfig.height * 0.1f;
            Vector3 newScale = new Vector3(x, 1, z);

            Debug.Log($"Config {gameObject.name} to new scale {newScale}");
            transform.localScale = newScale;
        }

        private void ConfigPlaneMaterial()
        {
            float x = mapConfig.width * 0.5f;
            float y = mapConfig.height * 0.5f;
            Vector2 textureScale = new Vector2(x, y);
            mapConfig.planeMaterial.mainTextureScale = textureScale;

            Debug.Log($"Config {gameObject.name} material tiling to {textureScale}");

            var mr = GetComponent<MeshRenderer>();
            mr.material = mapConfig.planeMaterial;
        }
    }
}
