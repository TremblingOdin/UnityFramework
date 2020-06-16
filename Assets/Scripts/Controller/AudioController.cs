using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : Controller
{
    private bool mute = false;

    private float userVolume = 100;

    public Dictionary<int, AudioData> AudioLibrary { get; private set; }

    public static AudioController Instance { get; private set; } = new AudioController();
    public AudioHelper AudioObject { get; private set; }

    private AudioController()
    {
        if(Instance != null)
        {
            return;
        }

        title = GameTypeTitle.AUDIO;
        AudioLibrary = new Dictionary<int, AudioData>();

        SceneManager.sceneLoaded += SceneLoaded;
    }

    protected override void SceneLoaded(Scene s, LoadSceneMode lsm)
    {
        if (GameController.Instance.UserSettings[UserSetting.VOLUME] as float? != null)
        {
            userVolume = (float)GameController.Instance.UserSettings[UserSetting.VOLUME];
        }
        else
        {
            userVolume = .5f;
        }
        CameraController.Instance.Cam.GetComponent<AudioSource>().volume = userVolume;
        CameraController.Instance.Cam.GetComponent<AudioSource>().Play();

        Debug.Log(AudioObject);
        Debug.Log("AudioController Loaded");
    }

    /// <summary>
    /// When called switches the value of mute and toggles in game sound
    /// </summary>
    /// <returns>If muted</returns>
    public bool ToggleMute()
    {
        mute = !mute;
        if (mute) {
            CameraController.Instance.Cam.GetComponent<AudioSource>().Pause();
            AudioListener.pause = mute;
            AudioListener.volume = 0;
        } else
        {
            AudioListener.pause = mute;
            AudioListener.volume = userVolume;
            CameraController.Instance.Cam.GetComponent<AudioSource>().UnPause();
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
