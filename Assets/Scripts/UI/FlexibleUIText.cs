using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


[RequireComponent(typeof(TextMeshPro))]
[RequireComponent(typeof(Font))]
public class FlexibleUIText : FlexibleUI
{
    public enum TextType
    {
        Default,
        Fancy,
        Menu,
        ToolTip
    }

    TextMeshProUGUI tmp;
    //Not all text will have fancy drawings and such
    Image fancyHappenings;
    public TextType tt;

    public override void OnSkinUI()
    {
        base.OnSkinUI();

        if(dontChange)
        {
            return;
        }

        tmp = GetComponent<TextMeshProUGUI>();

        fancyHappenings = transform.GetChild(0).GetComponent<Image>();

        switch(tt)
        {
            case TextType.Fancy:
                FancySetup();
                break;
            case TextType.Menu:
                MenuSetup();
                break;
            case TextType.ToolTip:
                ToolTipSetup();
                break;
            default:
                DefaultSetup();
                break;
        }
    }

    private void FancySetup()
    {
        tmp.font = skinData.fancyText.font;
        tmp.colorGradient = skinData.fancyText.colorGradient;
        tmp.outlineColor = skinData.fancyText.outlineColor;
        tmp.outlineWidth = skinData.fancyText.outlineWidth;
        tmp.fontSize = skinData.fancyText.fontSize;
        tmp.alignment = skinData.fancyText.alignment;
        tmp.fontStyle = skinData.fancyText.fontStyle;

        fancyHappenings.sprite = skinData.fancyHappenings;
        fancyHappenings.enabled = true;
    }

    private void MenuSetup()
    {
        tmp.font = skinData.menuText.font;
        tmp.colorGradient = skinData.menuText.colorGradient;
        tmp.outlineColor = skinData.menuText.outlineColor;
        tmp.outlineWidth = skinData.menuText.outlineWidth;
        tmp.fontSize = skinData.menuText.fontSize;
        tmp.alignment = skinData.menuText.alignment;
        tmp.fontStyle = skinData.menuText.fontStyle;

        fancyHappenings.enabled = false;
    }

    private void ToolTipSetup()
    {
        tmp.font = skinData.tooltipText.font;
        tmp.colorGradient = skinData.tooltipText.colorGradient;
        tmp.outlineColor = skinData.tooltipText.outlineColor;
        tmp.outlineWidth = skinData.tooltipText.outlineWidth;
        tmp.fontSize = skinData.tooltipText.fontSize;
        tmp.alignment = skinData.tooltipText.alignment;
        tmp.fontStyle = skinData.tooltipText.fontStyle;

        fancyHappenings.sprite = skinData.tooltipImage;
        fancyHappenings.enabled = true;
    }

    private void DefaultSetup()
    {
        tmp.font = skinData.defaultText.font;
        tmp.colorGradient = skinData.defaultText.colorGradient;
        tmp.outlineColor = skinData.defaultText.outlineColor;
        tmp.outlineWidth = skinData.defaultText.outlineWidth;
        tmp.fontSize = skinData.defaultText.fontSize;
        tmp.alignment = skinData.defaultText.alignment;
        tmp.fontStyle = skinData.defaultText.fontStyle;

        fancyHappenings.enabled = false;
    }
}
