using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Action OnRoadPlacement, OnHousePlacement, OnSpecialPlacement;
    public Button placeRoadButton, placeHouseButton, placeSpecialButton;

    public Color outlineColor;
    List<Button> buttonList;

    private void Start()
    {
        buttonList = new List<Button> { placeHouseButton, placeRoadButton, placeSpecialButton };

        placeHouseButton.onClick.AddListener(() => {
            ResetButtonsColor();
            ModifyOutline(placeHouseButton);
            OnHousePlacement?.Invoke();
        });

        placeRoadButton.onClick.AddListener(() => {
            ResetButtonsColor();
            ModifyOutline(placeRoadButton);
            OnRoadPlacement?.Invoke();
        });

        placeSpecialButton.onClick.AddListener(() => {
            ResetButtonsColor();
            ModifyOutline(placeSpecialButton);
            OnSpecialPlacement?.Invoke();
        });
    }

    private void ModifyOutline(Button button)
    {
        var outline = button.GetComponent<Outline>();
        outline.effectColor = outlineColor;
        outline.enabled = true;
    }

    private void ResetButtonsColor()
    {
        foreach (var button in buttonList)
        {
            button.GetComponent<Outline>().enabled = false;
        }
    }
}
