using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleUIDropdown : FlexibleUI
{
    public enum DropDownType
    {
        Default,
        Graphics
    }


    public DropDownType ddt;
    private Dropdown droppy;
    private Image image;

    public override void OnSkinUI()
    {
        base.OnSkinUI();

        if (dontChange)
        {
            return;
        }

        image = GetComponent<Image>();
        droppy = GetComponent<Dropdown>();

        switch(ddt)
        {
            case DropDownType.Graphics:
                GraphicsSetup();
                break;
            default:
                DefaultSetup();
                break;
        }
    }

    private void GraphicsSetup()
    {
        image.color = skinData.dropImageGraphicsColor;

        ColorBlock graphicsCB = new ColorBlock();

        graphicsCB.normalColor = skinData.graphicsDropColor;
        graphicsCB.highlightedColor = skinData.graphicsDropHighlightColor;
        graphicsCB.pressedColor = skinData.graphicsDropPressedColor;
        graphicsCB.selectedColor = skinData.graphicsDropSelectedColor;
        graphicsCB.disabledColor = skinData.graphicsDropDisabledColor;

        droppy.colors = graphicsCB;
    }

    private void DefaultSetup()
    {
        image.color = skinData.dropImageColor;

        ColorBlock defaultCB = new ColorBlock();

        defaultCB.normalColor = skinData.defaultDropColor;
        defaultCB.highlightedColor = skinData.defaultDropHighlightColor;
        defaultCB.pressedColor = skinData.defaultDropPressedColor;
        defaultCB.selectedColor = skinData.defaultDropSelectedColor;
        defaultCB.disabledColor = skinData.defaultDropDisabledColor;

        droppy.colors = defaultCB;
    }
}
