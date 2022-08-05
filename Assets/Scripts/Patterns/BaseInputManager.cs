using System;
using UnityEngine;

public abstract class BaseInputManager : MonoBehaviour, IInput
{
    public Action<Ray> OnMouseClick, OnMouseHold;
    public Action OnMouseUp, OnEscape;

    protected Vector2 cameraMovementVector;
    public Vector2 CameraMovementVector => cameraMovementVector;

    public abstract void CheckArrowInput();
    public abstract void CheckClickDownEvent();
    public abstract void CheckClickHoldEvent();
    public abstract void CheckClickUpEvent();
    public abstract void CheckEscapeInput();

    private void Update()
    {
        CheckClickDownEvent();
        CheckClickUpEvent();
        CheckClickHoldEvent();
        CheckArrowInput();
        CheckEscapeInput();
    }

    public void ClearEvents()
    {
        OnMouseClick = null;
        OnMouseHold = null;
        OnMouseUp = null;
        OnEscape = null;
    }
}