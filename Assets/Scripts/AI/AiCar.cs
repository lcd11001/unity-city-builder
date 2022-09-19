using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AiCar : MonoBehaviour, IAiBehaviour
{
    [SerializeField] private List<Vector3> path = null;

    [SerializeField] private float arriveDistance = .3f;
    [SerializeField] private float lastPointArriveDistance = .1f;
    [SerializeField] private float turningAngleOffset = 5f;
    [SerializeField] private Vector3 currentTargetPosition;

    [SerializeField] private Color pathColor;
    PathVisualizer pathVisualizer;

    private int index = 0;

    private bool stop;

    public event OnDeathHandler OnDeath;
    public Vector3 Position => transform.position;

    public bool Stop
    {
        get { return stop; }
        set { stop = value; }
    }

    [field: SerializeField]
    public UnityEvent<Vector3> OnDrive { get; set; }


    private void Start()
    {
        pathVisualizer = FindObjectOfType<PathVisualizer>();
        pathColor = UnityEngine.Random.ColorHSV(0f, 1f, 0f, 1f, 0f, 1f);

        if (path == null || path.Count == 0)
        {
            Stop = true;
        }
        else
        {
            currentTargetPosition = path[index];
        }
    }

    public void ShowPath()
    {
        pathVisualizer.ShowPath(path, this, pathColor);
    }

    public void SetPath(List<Vector3> path)
    {
        if (path.Count == 0)
        {
            Destroy(gameObject);
            return;
        }

        this.path = path;
        index = 0;
        currentTargetPosition = this.path[index];

        Vector3 relativePoint = transform.InverseTransformPoint(this.path[index + 1]);
        float angle = Mathf.Atan2(relativePoint.x, relativePoint.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, angle, 0);

        Stop = false;
    }

    private void Update()
    {
        CheckIfArrived();
        Drive();
    }

    private void Drive()
    {
        if (Stop)
        {
            OnDrive?.Invoke(Vector3.zero);
        }
        else
        {
            Vector3 relativePoint = transform.InverseTransformPoint(currentTargetPosition);
            float angle = Mathf.Atan2(relativePoint.x, relativePoint.z) * Mathf.Rad2Deg;
            var rotateCar = 0;
            if (angle > turningAngleOffset)
            {
                rotateCar = 1;
            }
            else if (angle < -turningAngleOffset)
            {
                rotateCar = -1;
            }
            OnDrive?.Invoke(new Vector3(rotateCar, 0, 1));
        }
    }

    private void CheckIfArrived()
    {
        if (Stop == false)
        {
            var distanceToCheck = arriveDistance;
            if (index == path.Count - 1)
            {
                distanceToCheck = lastPointArriveDistance;
            }
            if (Vector3.Distance(currentTargetPosition, transform.position) < distanceToCheck)
            {
                SetNextTargetIndex();
            }
        }
    }

    private void SetNextTargetIndex()
    {
        index++;
        if (index >= path.Count)
        {
            Stop = true;
            Destroy(gameObject);
        }
        else
        {
            currentTargetPosition = path[index];
        }
    }

    private void OnDestroy()
    {
        OnDeath?.Invoke();
    }
}
