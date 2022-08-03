using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputDragManager : BaseInputManager
{
    [SerializeField] Camera mainCamera;

    public LayerMask groundMask;

    private bool isHolding = false;
    private Vector3Int startPosition, endPosition;

    private Vector3Int? RaycastGround()
    {
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, groundMask))
        {
            Vector3Int positionInt = Vector3Int.RoundToInt(hit.point);
            return positionInt;
        }

        return null;
    }

    public override void CheckArrowInput()
    {
        if (isHolding)
        {
            Vector3Int dragDelta = (endPosition - startPosition);
            cameraMovementVector = new Vector2(-dragDelta.x, -dragDelta.z);
        }
        else
        {
            cameraMovementVector = Vector2.zero;
        }
    }

    public override void CheckClickHoldEvent()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if (position != null)
            {
                isHolding = true;
                endPosition = position.Value;

                OnMouseHold?.Invoke(position.Value);
            }
        }
    }

    public override void CheckClickUpEvent()
    {
        if (Input.GetMouseButtonUp(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            isHolding = false;

            OnMouseUp?.Invoke();
        }
    }

    public override void CheckClickDownEvent()
    {
        if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            var position = RaycastGround();
            if (position != null)
            {
                isHolding = false;
                startPosition = position.Value;

                OnMouseClick?.Invoke(position.Value);
            }
        }
    }
}
