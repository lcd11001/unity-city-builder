using System;
using UnityEngine;

public abstract class BaseInputManager : MonoBehaviour, IInput
{
    public Action<Vector3Int> OnMouseClick, OnMouseHold;
    public Action OnMouseUp;

    protected Vector2 cameraMovementVector;
    public Vector2 CameraMovementVector => cameraMovementVector;

    public abstract void CheckArrowInput();
    public abstract void CheckClickDownEvent();
    public abstract void CheckClickHoldEvent();
    public abstract void CheckClickUpEvent();

    private void Update()
    {
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
        CheckArrowInput();
    }
}