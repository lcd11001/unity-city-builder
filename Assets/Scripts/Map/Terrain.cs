using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CityBuilder.Map
{
    public class Terrain : MonoBehaviour
    {
        [SerializeField] SO_MapConfig mapConfig;
        [SerializeField] Ground mapGround;
        [SerializeField] Plane mapPlane;

        private void OnValidate()
        {
            ConfigTerrain();
            ConfigGround();
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

            Debug.Log($"Config {mapPlane.gameObject.name} to new scale {newScale}");
            mapPlane.transform.localScale = newScale;
        }

        private void ConfigPlaneMaterial()
        {
            float x = mapConfig.width * 0.5f;
            float y = mapConfig.height * 0.5f;
            Vector2 textureScale = new Vector2(x, y);
            mapConfig.planeMaterial.mainTextureScale = textureScale;

            Debug.Log($"Config {mapPlane.gameObject.name} material tiling to {textureScale}");

            var mr = mapPlane.GetComponent<MeshRenderer>();
            mr.material = mapConfig.planeMaterial;
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

            Debug.Log($"Config {mapGround.gameObject.name} to new scale {newScale}");
            mapGround.transform.localScale = newScale;

            Vector3 newPosition = new Vector3(0, (y * -0.5f) - 0.01f, 0);
            Debug.Log($"Config {mapGround.gameObject.name} to new position {newPosition}");
            mapGround.transform.localPosition = newPosition;
        }

        private void ConfigGroundMaterial()
        {
            var mr = mapGround.GetComponent<MeshRenderer>();
            mr.material = mapConfig.groundMaterial;
        }

        private void ConfigTerrain()
        {
            float x = (mapConfig.width - 1) * 0.5f;
            float z = (mapConfig.height - 1) * 0.5f;
            Vector3 newPosition = new Vector3(x, 0, z);
            transform.position = newPosition;
            Debug.Log($"Config {gameObject.name} to new position {newPosition}");
        }
    }
}
