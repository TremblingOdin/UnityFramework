using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu(menuName = "Flexible UI Data")]
public class FlexibleUIData : ScriptableObject
{
    [Header("Button Settings")]
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

    public Color menuColor;

    public Color settingColor;
    public Sprite settingSprite;

    public Sprite downArrowSprite;
    public Sprite upArrowSprite;
    public Sprite rightArrowSprite;
    public Sprite leftArrowSprite;

    [Header("Text Settings")]
    //Need a whole ass gameobject to store a textmesh pro reference
    public TextMeshProUGUI defaultText;

    public TextMeshProUGUI fancyText;
    public Sprite fancyHappenings;

    public TextMeshProUGUI menuText;

    public TextMeshProUGUI tooltipText;
    public Sprite tooltipImage;

    [Header("Dropdown Settings")]
    public Color defaultDropColor;
    public Color defaultDropHighlightColor;
    public Color defaultDropPressedColor;
    public Color defaultDropSelectedColor;
    public Color defaultDropDisabledColor;

    public Color dropImageColor;

    public Color graphicsDropColor;
    public Color graphicsDropHighlightColor;
    public Color graphicsDropPressedColor;
    public Color graphicsDropSelectedColor;
    public Color graphicsDropDisabledColor;

    public Color dropImageGraphicsColor;

    [Header("Toggle Settings")]
    public Color defaultTextToggle;
    public Color defaultCheckToggle;
    public Color defaultToggleColor;
    public Color defaultToggleHighlightColor;
    public Color defaultTogglePressedColor;
    public Color defaultToggleSelectedColor;
    public Color defaultToggleDisabledColor;

    public Sprite defaultToggleImage;
    public float defaultToggleFontSize;
    public TextAlignmentOptions defaultToggleAlignment;
    public FontStyles defaultToggleFontStyle;

    public Color menuTextToggle;
    public Color menuCheckToggle;
    public Color menuToggleColor;
    public Color menuToggleHighlightColor;
    public Color menuTogglePressedColor;
    public Color menuToggleSelectedColor;
    public Color menuToggleDisabledColor;

    public Sprite menuToggleImage;
    public float menuToggleFontSize;
    public TextAlignmentOptions menuToggleAlignment;
    public FontStyles menuToggleFontStyle;
}
