using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
[RequireComponent(typeof(Font))]
public class FlexibleUIText : FlexibleUI
{
    public enum TextType
    {
        Default,
        ToolTip,
        Fancy
    }

    Font font;
    //Not all text will have fancy drawings and such
    Sprite fancyHappenings;
    public TextType tt;

    public override void OnSkinUI()
    {
        base.OnSkinUI();

        font = GetComponent<Text>().font;

        if(GetComponent<Sprite>() != null)
        {
            fancyHappenings = GetComponent<Sprite>();
        }

        switch(tt)
        {
            case TextType.Fancy:
                FancySetup();
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
        font = skinData.fancyFont;
        fancyHappenings = skinData.fancyHappenings;
    }

    private void ToolTipSetup()
    {
        font = skinData.tooltipFont;
        fancyHappenings = skinData.tooltipImage;
    }

    private void DefaultSetup()
    {
        font = skinData.defaultFont;
    }
}
