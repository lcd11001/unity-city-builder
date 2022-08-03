using System;
using UnityEngine;

public interface IInput
{
    Vector2 CameraMovementVector { get; }
    void CheckClickDownEvent();
    void CheckClickUpEvent();
    void CheckClickHoldEvent();
    void CheckArrowInput();
}