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
    private Vector3 startPosition, endPosition;

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
        if (Input.GetMouseButtonDown(0))
        {
            isHolding = true;
            startPosition = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isHolding = false;
        }

        if (isHolding)
        {
            endPosition = Input.mousePosition;
            Vector3 delta = (endPosition - startPosition);
            Vector3 dragDelta = delta.normalized * 1;

            cameraMovementVector = new Vector2(-dragDelta.x, -dragDelta.y);

            startPosition = endPosition;
        }
        else
        {
            cameraMovementVector = Vector2.zero;
        }
        
        /*
        if (Input.touchCount > 0)
        {
            cameraMovementVector = Input.GetTouch(0).deltaPosition;
            Debug.Log($"cameraMovementVector {cameraMovementVector}");
        }
        else
        {
            cameraMovementVector = Vector2.zero;
        }
        */
    }

    public override void CheckClickHoldEvent()
    {
        if (Input.GetMouseButton(0) && EventSystem.current.IsPointerOverGameObject() == false)
        {
            OnMouseHold?.Invoke(mainCamera.ScreenPointToRay(Input.mousePosition));
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
            OnMouseClick?.Invoke(mainCamera.ScreenPointToRay(Input.mousePosition));
        }
    }

    public override void CheckEscapeInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            OnEscape?.Invoke();
        }
    }
}
