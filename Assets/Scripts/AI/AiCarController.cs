using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class AiCarController : MonoBehaviour
{
    Rigidbody rb;

    [SerializeField] private float power = 5.0f;
    [SerializeField] private float torque = 0.5f;
    [SerializeField] private float maxSpeed = 5f;

    [SerializeField] private Vector3 movementVector;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    public void Move(Vector3 movementInput)
    {
        this.movementVector = movementInput;
    }

    private void FixedUpdate()
    {
        if (rb.velocity.magnitude < maxSpeed)
        {
            // move forward by Z value
            rb.AddForce(movementVector.z * transform.forward * power);
        }

        // turn left-right by X value
        // and NOT turn in place when not moving if Z value = 0
        rb.AddTorque(movementVector.x * Vector3.up * torque * movementVector.z);
    }
}
