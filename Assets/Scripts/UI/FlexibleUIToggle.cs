using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FlexibleUIToggle : FlexibleUI
{
    public enum ToggleType
    {
        Default,
        Menu
    }

    public ToggleType tt;
    private ColorBlock cb;
    private Image checkboxImage;
    private TextMeshProUGUI textmesh;

    public override void OnSkinUI()
    {
        base.OnSkinUI();

        if(dontChange)
        {
            return;
        }

        cb = new ColorBlock();
        textmesh = transform.GetComponentInChildren<TextMeshProUGUI>();
        checkboxImage = transform.GetChild(0).GetComponent<Image>();

        switch (tt)
        {
            case ToggleType.Menu:
                MenuSetup();
                break;
            default:
                DefaultSetup();
                break;
        }
    }

    private void MenuSetup()
    {

        cb.normalColor = skinData.menuToggleColor;
        cb.highlightedColor = skinData.menuToggleHighlightColor;
        cb.pressedColor = skinData.menuTogglePressedColor;
        cb.selectedColor = skinData.menuToggleSelectedColor;
        cb.disabledColor = skinData.menuToggleDisabledColor;

        GetComponent<Toggle>().colors = cb;

        textmesh.fontSize = skinData.menuToggleFontSize;
        textmesh.alignment = skinData.menuToggleAlignment;
        textmesh.fontStyle = skinData.menuToggleFontStyle;

        checkboxImage.sprite = skinData.menuToggleImage;
        checkboxImage.color = cb.selectedColor;
    }

    private void DefaultSetup()
    {
        cb.normalColor = skinData.defaultToggleColor;
        cb.highlightedColor = skinData.defaultToggleHighlightColor;
        cb.pressedColor = skinData.defaultTogglePressedColor;
        cb.selectedColor = skinData.defaultToggleSelectedColor;
        cb.disabledColor = skinData.defaultToggleDisabledColor;

        GetComponent<Toggle>().colors = cb;

        textmesh.fontSize = skinData.defaultToggleFontSize;
        textmesh.alignment = skinData.defaultToggleAlignment;
        textmesh.fontStyle = skinData.defaultToggleFontStyle;

        checkboxImage.sprite = skinData.defaultToggleImage;
        checkboxImage.color = cb.selectedColor;
    }
}
