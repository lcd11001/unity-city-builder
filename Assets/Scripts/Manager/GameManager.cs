using System;
using System.Collections;
using System.Collections.Generic;
using SVS;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public BaseInputManager inputManager;
    public RoadManager roadManager;

    private void Start()
    {
        inputManager.OnMouseClick += HandleMouseClick;
        inputManager.OnMouseHold += HandleMouseHold;
        inputManager.OnMouseUp += HandleMouseUp;
    }

    private void HandleMouseUp()
    {
        // Debug.Log($"HandleMouseUp");
        roadManager.FinishPlacingRoad();
    }

    private void HandleMouseHold(Vector3Int position)
    {
        // Debug.Log($"HandleMouseHold {position}");
        roadManager.PlaceRoad(position);
    }

    private void HandleMouseClick(Vector3Int position)
    {
        // Debug.Log($"HandleMouseClick {position}");
        roadManager.PlaceRoad(position);
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
    }
}
