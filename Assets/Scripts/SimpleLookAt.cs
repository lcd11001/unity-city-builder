using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleLookAt : MonoBehaviour
{
    public Transform target;

    private void Start()
    {
        target = Camera.main.gameObject.transform;
    }

    private void Update()
    {
        // transform.LookAt(target);
        transform.LookAt(transform.position + target.transform.rotation * Vector3.forward, target.transform.rotation * Vector3.up);
    }
}
