using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnitTestDemo
{
    [RequireComponent(typeof(Rigidbody))]
    public class Player : MonoBehaviour
    {
        private Rigidbody _rigidbody;

        [SerializeField]
        private float moveSpeed;
        [SerializeField]
        private float moveTime;
        [SerializeField]
        private bool useMoveTime;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;

            var inputs = GetComponents<IInput>();
            foreach(var input in inputs)
            {
                if (input.IsActive)
                {
                    PlayerInput = input;
                    Debug.Log($"PlayerInput {PlayerInput.GetType().Name}");
                }
            }
        }

        public IInput PlayerInput { get; set; }

        private void Update()
        {
            if (!useMoveTime || moveTime > 0)
            {
                moveTime -= Time.deltaTime;
                float vertical = PlayerInput.Vertical;
                // _rigidbody.AddForce(0, 0, vertical * moveSpeed);
                _rigidbody.velocity = new Vector3(0, 0, vertical * moveSpeed);
            }
            else
            {
                _rigidbody.velocity = Vector3.zero;
            }
        }
    }
}
