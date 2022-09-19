using System;
using System.Collections;
using System.Collections.Generic;
using CityBuilder.AI;
using SVS;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public CameraMovement cameraMovement;
    public BaseInputManager inputManager;
    public RoadManager roadManager;
    public StructureManager structureManager;
    public UIManager uiManager;
    public ObjectDetector objectDetector;
    public PathVisualizer pathVisualizer;

    private void Start()
    {
        uiManager.OnRoadPlacement += RoadPlacementHandler;
        uiManager.OnHousePlacement += HousePlacementHandler;
        uiManager.OnSpecialPlacement += SpecialPlacementHandler;
        uiManager.OnBigStructurePlacement += BigStructurePlacementHandler;
        inputManager.OnEscape += EscapeHandler;
    }

    private void EscapeHandler()
    {
        ClearInputActions();
        uiManager.ResetButtonsColor();
        pathVisualizer.ResetPath();
        inputManager.OnMouseClick += TrySelectingAgent;
    }

    private void TrySelectingAgent(Ray ray)
    {
        GameObject hitObject = objectDetector.RaycastAll(ray);
        if (hitObject != null)
        {
            var agent = hitObject.GetComponent<IAiBehaviour>();
            agent?.ShowPath();
        }
    }

    private void BigStructurePlacementHandler()
    {
        ClearInputActions();
        inputManager.OnMouseClick += (pos) => {
            ProcessInputAndCall(structureManager.PlaceBigStructure, pos);
        };
        inputManager.OnEscape += EscapeHandler;
    }

    private void SpecialPlacementHandler()
    {
        ClearInputActions();

        inputManager.OnMouseClick += (pos) => {
            ProcessInputAndCall(structureManager.PlaceSpecial, pos);
        };
        inputManager.OnEscape += EscapeHandler;
    }

    private void HousePlacementHandler()
    {
        ClearInputActions();

        inputManager.OnMouseClick += (pos) => {
            ProcessInputAndCall(structureManager.PlaceHouse, pos);
        };
        inputManager.OnEscape += EscapeHandler;
    }

    private void RoadPlacementHandler()
    {
        ClearInputActions();

        inputManager.OnMouseClick += (pos) => {
            ProcessInputAndCall(roadManager.PlaceRoad, pos);
        };
        inputManager.OnMouseHold += (pos) => {
            ProcessInputAndCall(roadManager.PlaceRoad, pos);
        };
        inputManager.OnMouseUp += roadManager.FinishPlacingRoad;

        inputManager.OnEscape += EscapeHandler;
    }

    private void ClearInputActions()
    {
        inputManager.ClearEvents();
    }

    private void ProcessInputAndCall(Action<Vector3Int> callback, Ray ray)
    {
        Vector3Int? result = objectDetector.RaycastGround(ray);
        if (result.HasValue)
        {
            callback.Invoke(result.Value);
        }
    }

    private void Update()
    {
        cameraMovement.MoveCamera(new Vector3(inputManager.CameraMovementVector.x, 0, inputManager.CameraMovementVector.y));
    }
}
