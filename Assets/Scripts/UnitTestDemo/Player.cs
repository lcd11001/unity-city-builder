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

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _rigidbody.useGravity = false;

            PlayerInput = GetComponent<IInput>();
        }

        public IInput PlayerInput { get; set; }

        private void Update()
        {
            float vertical = PlayerInput.Vertical;
            // _rigidbody.AddForce(0, 0, vertical * moveSpeed);
            _rigidbody.velocity = new Vector3(0, 0, vertical * moveSpeed);
        }
    }
}
