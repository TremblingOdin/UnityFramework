using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : Controller
{
    private bool mute = false;

    private float userVolume = 100;

    public static AudioController Instance { get; private set; } = new AudioController();
    public AudioHelper AudioObject { get; private set; }

    private AudioController()
    {
        if(Instance != null)
        {
            return;
        }

        title = GameTypeTitle.AUDIO;
    }

    private void Start()
    {
        if (GameController.Instance.UserSettings[UserSetting.VOLUME] as float? != null)
        {
            userVolume = (float)GameController.Instance.UserSettings[UserSetting.VOLUME];
        } else
        {
            userVolume = .5f;
        }
        CameraController.Instance.Cam.GetComponent<AudioSource>().volume = userVolume;
        CameraController.Instance.Cam.GetComponent<AudioSource>().Play();
        //Invoke("TrackChange", GameController.Instance.GetType<CameraController>(GameTypeTitle.CAMERA).gameObject.GetComponent<AudioSource>().clip.length);
    }

    /// <summary>
    /// Change the intro track to the looping track
    /// </summary>
    private void TrackChange()
    {
        CameraController.Instance.Cam.GetComponent<AudioSource>().Stop();
        CameraController.Instance.Cam.GetComponent<AudioSource>().clip = AudioObject.INTROLESS_LEVEL_SONG;
        Debug.Log(CameraController.Instance.Cam.GetComponent<AudioSource>().clip);
        CameraController.Instance.Cam.GetComponent<AudioSource>().loop = true;
        CameraController.Instance.Cam.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// When called switches the value of mute and toggles in game sound
    /// </summary>
    /// <returns>If muted</returns>
    public bool ToggleMute()
    {
        mute = !mute;
        if (mute) {
            AudioListener.pause = mute;
            AudioListener.volume = 0;
        } else
        {
            AudioListener.pause = mute;
            AudioListener.volume = userVolume;
        }

        return mute;
    }

    /// <summary>
    /// When sent a new Volume value update current volume value
    /// </summary>
    /// <param name="volume">New Volume Value</param>
    /// <returns></returns>
    public void ChangeVolume(float volume)
    {
        AudioListener.volume = volume;
        userVolume = volume;
    }

    /// <summary>
    /// Sets the AudioHelper to the provided object and returns if successful or not
    /// </summary>
    /// <param name="link">AudioHelper to be linked</param>
    /// <returns>Success of the linkage</returns>
    public bool LinkAudioObject(AudioHelper link)
    {
        AudioObject = link;

        if(AudioObject == link)
        {
            return true;
        } else
        {
            return false;
        }
    }
}
