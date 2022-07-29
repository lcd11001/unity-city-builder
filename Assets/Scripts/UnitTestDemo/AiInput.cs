using UnityEngine;

namespace UnitTestDemo
{
    public class AiInput : MonoBehaviour, IInput
    {
        public float Vertical => 1.0f;

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