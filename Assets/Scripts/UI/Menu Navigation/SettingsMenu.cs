using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public Slider volumeSlide;
    private float userVolume;

    public TMP_Dropdown qualityDrop;
    private int qualityIndex;

    public FlexibleUIToggle flexiFullscreen;
    private bool fullscreen;

    private Resolution resolution;
    public TMP_Dropdown resolutionDropdown;
    private int selectedRes;
    Resolution[] resolutions;

    void Start()
    {
        GameController.Instance.LoadSettings();

        if (GameController.Instance.UserSettings[UserSetting.VOLUME] as float? != null)
        {
            userVolume = (float)GameController.Instance.UserSettings[UserSetting.VOLUME];
            volumeSlide.value = userVolume;
            SetVolume(userVolume);
        }

        if (GameController.Instance.UserSettings[UserSetting.QUALITY] as int? != null && (int)GameController.Instance.UserSettings[UserSetting.QUALITY] < QualitySettings.names.Length)
        {
            qualityIndex = (int)GameController.Instance.UserSettings[UserSetting.QUALITY];
            qualityDrop.value = qualityIndex;
            SetQuality(qualityIndex);
        } else
        {
            qualityIndex = QualitySettings.GetQualityLevel();
            qualityDrop.value = qualityIndex;
            SetQuality(qualityIndex);
        }

        if(GameController.Instance.UserSettings[UserSetting.FULLSCREEN] as bool? != null)
        {
            fullscreen = (bool)GameController.Instance.UserSettings[UserSetting.FULLSCREEN];
            flexiFullscreen.GetComponent<Toggle>().isOn = fullscreen;
            SetFullScreen(fullscreen);
        }


        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> resStrings = new List<string>();

        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;

            //Using as Resolution? let's it be cast to null if a garbage value is in the user setting
            if ((GameController.Instance.UserSettings[UserSetting.RESOLUTION] as Resolution?) != null 
                && ((Resolution)GameController.Instance.UserSettings[UserSetting.RESOLUTION]).width == resolutions[i].width
                && ((Resolution)GameController.Instance.UserSettings[UserSetting.RESOLUTION]).height == resolutions[i].height) {
                selectedRes = i;
                resolution = resolutions[i];
            }

            resStrings.Add(option);
        }

        resolutionDropdown.AddOptions(resStrings);
        resolutionDropdown.value = selectedRes;
    }

    public void SetVolume(float volume)
    {
        userVolume = volume;
        AudioController.Instance.ChangeVolume(userVolume);
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
        this.qualityIndex = qualityIndex;
    }

    public void SetResolution(int resolutionIndex)
    {
        resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullScreen(bool fullscreen)
    {
        this.fullscreen = fullscreen;
        Screen.fullScreen = fullscreen;
    }

    public void SaveSettings()
    {
        GameController.Instance.UserSettings[UserSetting.VOLUME] = userVolume;
        GameController.Instance.UserSettings[UserSetting.QUALITY] = qualityIndex;
        GameController.Instance.UserSettings[UserSetting.FULLSCREEN] = fullscreen;
        GameController.Instance.UserSettings[UserSetting.RESOLUTION] = resolution;
        GameController.Instance.SaveSettings();
    }
}
