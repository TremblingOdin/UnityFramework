using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
[RequireComponent(typeof(Image))]
public class FlexibleUIButton : FlexibleUI
{
    public enum ButtonType
    {
        Default,
        Cancel,
        DownArrow,
        Home,
        LeftArrow,
        Menu,
        RightArrow,
        Setting,
        Sound,
        UpArrow
    }

    Image image;
    Image icon;
    Button button;
    public ButtonType bt;

    public override void OnSkinUI()
    {
        base.OnSkinUI();

        if (dontChange)
        {
            return;
        }

        image = GetComponent<Image>();
        icon = transform.GetChild(0).GetComponent<Image>();
        button = GetComponent<Button>();

        button.transition = Selectable.Transition.SpriteSwap;
        button.targetGraphic = image;

        image.sprite = skinData.buttonSprite;
        image.type = Image.Type.Sliced;
        button.spriteState = skinData.buttonSpriteState;

        button.onClick.RemoveAllListeners();

        switch(bt)
        {
            case ButtonType.DownArrow:
                DownArrowSetup();
                break;
            case ButtonType.UpArrow:
                UpArrowSetup();
                break;
            case ButtonType.RightArrow:
                RightArrowSetup();
                break;
            case ButtonType.LeftArrow:
                LeftArrowSetup();
                break;
            case ButtonType.Cancel:
                CancelSetup();
                break;
            case ButtonType.Home:
                HomeSetup();
                break;
            case ButtonType.Menu:
                MenuSetup();
                break;
            case ButtonType.Setting:
                SettingSetup();
                break;
            case ButtonType.Sound:
                SoundSetup();
                break;
            default:
                DefaultSetup();
                break;
        }

        transform.GetChild(0).GetComponent<RectTransform>()
            .SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, GetComponent<RectTransform>().rect.width * .75f);

        transform.GetChild(0).GetComponent<RectTransform>()
    .SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, GetComponent<RectTransform>().rect.height * .75f);
    }

    //The Arrows should be rewritten with each project in order to add their on click behaviors or you can just add the onclick behaviors in the GUI
    private void DownArrowSetup()
    {
        image.color = skinData.defaultColor;
        icon.sprite = skinData.downArrowSprite;
    }

    private void UpArrowSetup()
    {
        image.color = skinData.defaultColor;
        icon.sprite = skinData.upArrowSprite;
    }

    private void LeftArrowSetup()
    {
        image.color = skinData.defaultColor;
        icon.sprite = skinData.leftArrowSprite;
    }

    private void RightArrowSetup()
    {
        image.color = skinData.defaultColor;
        icon.sprite = skinData.rightArrowSprite;
    }

    private void CancelSetup()
    {
        image.color = skinData.cancelColor;
        icon.sprite = skinData.cancelIcon;
        GetComponent<Button>().onClick.AddListener(CancelParent);
    }

    private void HomeSetup()
    {
        image.color = skinData.homeColor;
        icon.sprite = skinData.homeIcon;
        GetComponent<Button>().onClick.AddListener(CallHomeEvent);
    }

    private void MenuSetup()
    {
        image.color = skinData.menuColor;
        icon.sprite = null;
        icon.enabled = false;
    }

    private void SettingSetup()
    {
        image.color = skinData.settingColor;
        icon.sprite = skinData.settingSprite;
        GetComponent<Button>().onClick.AddListener(CallPauseEvent);
    }

    private void SoundSetup()
    {
        image.color = skinData.soundColor;
        icon.sprite = skinData.soundOnIcon;
        GetComponent<Button>().onClick.AddListener(CallMuteEvent);
    }

    private void DefaultSetup()
    {
        image.color = skinData.defaultColor;
        icon.sprite = skinData.defaultIcon;
    }

    /// <summary>
    /// Calls the mute event so that other game objects can handle it accordingly, then changes the sound icon
    /// </summary>
    private void CallMuteEvent()
    {
        EventService.Instance.HandleEvents(EventType.Mute);
        if(icon.sprite == skinData.soundOnIcon)
        {
            icon.sprite = skinData.soundOffIcon;
        } else
        {
            icon.sprite = skinData.soundOnIcon;
        }
    }

    /// <summary>
    /// Calls the Pause event so that other game objects can handle it accordingly, may need to be overwritten for some game to change the pause screen
    /// </summary>
    private void CallPauseEvent()
    {
        EventService.Instance.HandleEvents(EventType.Pause);
    }

    /// <summary>
    /// Calls the Home event so that other game objects can handle it accordingly, presumably this ends the level then returns to the main menu
    /// </summary>
    private void CallHomeEvent()
    {
        EventService.Instance.HandleEvents(EventType.Home);
    }

    /// <summary>
    /// Destroys the parent, this may need to be overwritten based on the parent...if it has a form being filled or something I dunno
    /// </summary>
    private void CancelParent()
    {
        Destroy(transform.parent.gameObject);
    }
}
