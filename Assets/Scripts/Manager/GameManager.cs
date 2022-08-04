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
    public UIManager uiManager;

    private void Start()
    {
        uiManager.OnRoadPlacement += RoadPlacementHandler;
        uiManager.OnHousePlacement += HousePlacementHandler;
        uiManager.OnSpecialPlacement += SpecialPlacementHandler;
    }

    private void SpecialPlacementHandler()
    {
        ClearInputAction();
    }

    private void HousePlacementHandler()
    {
        ClearInputAction();
    }

    private void RoadPlacementHandler()
    {
        ClearInputAction();

        inputManager.OnMouseClick += roadManager.PlaceRoad;
        inputManager.OnMouseHold += roadManager.PlaceRoad;
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;
    }

    private void ClearInputAction()
    {
        inputManager.OnMouseClick = null;
        inputManager.OnMouseHold = null;
        inputManager.OnMouseUp = null;
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
    }
}
