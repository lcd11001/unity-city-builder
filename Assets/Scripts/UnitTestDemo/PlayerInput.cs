using UnityEngine;

namespace UnitTestDemo
{
    public class PlayerInput : MonoBehaviour, IInput
    {
        public float Vertical => Input.GetAxis("Vertical");

        public bool IsActive => this.enabled;

        private void Start()
        {
            // Having only Start() also enables the on/off checker box from inspector
        }

        private void OnDisable()
        {
            Debug.Log($"{this.GetType().Name} OnDisable");
        }

        private void OnEnable()
        {
            Debug.Log($"{this.GetType().Name} OnEnable");
        }
    }
}