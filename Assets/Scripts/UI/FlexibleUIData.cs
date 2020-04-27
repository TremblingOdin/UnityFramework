using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;

[CreateAssetMenu(menuName = "Flexible UI Data")]
public class FlexibleUIData : ScriptableObject
{
    public Sprite buttonSprite;
    public SpriteState buttonSpriteState;

    public Color defaultColor;
    public Sprite defaultIcon;

    public Color soundColor;
    public Sprite soundOnIcon;
    public Sprite soundOffIcon;

    public Color cancelColor;
    public Sprite cancelIcon;

    public Color homeColor;
    public Sprite homeIcon;

    public Color settingColor;
    public Sprite settingSprite;

    public Sprite downArrowSprite;
    public Sprite upArrowSprite;
    public Sprite rightArrowSprite;
    public Sprite leftArrowSprite;

    public Font defaultFont;

    public Font fancyFont;
    public Sprite fancyHappenings;

    public Font tooltipFont;
    public Sprite tooltipImage;
}
