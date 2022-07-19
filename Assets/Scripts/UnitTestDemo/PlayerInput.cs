using UnityEngine;

namespace UnitTestDemo
{
    public class PlayerInput : MonoBehaviour, IInput
    {
        public float Vertical => Input.GetAxis("Vertical");
    }
}