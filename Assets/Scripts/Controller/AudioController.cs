using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioController : Controller
{
    private bool mute = false;

    private float userVolume = 100;

    public Dictionary<int, List<AudioData>> AudioLibrary { get; private set; }

    public static AudioController Instance { get; private set; } = new AudioController();
    public AudioHelper AudioObject { get; private set; }

    private AudioController()
    {
        if(Instance != null)
        {
            return;
        }

        title = GameTypeTitle.AUDIO;
        AudioLibrary = new Dictionary<int, List<AudioData>>();

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
        CameraController.Instance.Cam.Audio.volume = userVolume;
        CameraController.Instance.Cam.Audio.Play();

        if (FindClip(s.buildIndex, PlayCase.Intro) != null && LevelController.NewMusic)
        {
            AudioObject.QueueTrackChange(FindClip(s.buildIndex, PlayCase.Intro), FindClip(s.buildIndex));
        }
        else if (CameraController.Instance.Cam.Audio.clip != FindClip(s.buildIndex) && LevelController.NewMusic)
        {
            AudioObject.TrackChange(FindClip(s.buildIndex));
        }
    }

    /// <summary>
    /// Locates the Default Clip and returns it, if no defualt clip returns null
    /// </summary>
    /// <param name="scene">Scene to get the clip for</param>
    /// <returns>Audio clip sought</returns>
    public AudioClip FindClip(int scene)
    {
        if(!AudioLibrary.ContainsKey(scene))
        {
            return null;
        }

        foreach(AudioData ad in AudioLibrary[scene])
        {
            if(ad.playCase == PlayCase.Default)
            {
                return ad.clip;
            }
        }

        return null;
    }

    /// <summary>
    /// Locates the first clip in the scene with the provided use
    /// </summary>
    /// <param name="scene">Scene to search</param>
    /// <param name="use">PlayCase to search for</param>
    /// <returns>Audio clip sought</returns>
    public AudioClip FindClip(int scene, PlayCase use)
    {
        if(!AudioLibrary.ContainsKey(scene))
        {
            return null;
        }

        foreach(AudioData ad in AudioLibrary[scene])
        {
            if(ad.playCase == use)
            {
                return ad.clip;
            }
        }

        return null;
    }

    /// <summary>
    /// When called switches the value of mute and toggles in game sound
    /// </summary>
    /// <returns>If muted</returns>
    public bool ToggleMute()
    {
        mute = !mute;
        if (mute) {
            CameraController.Instance.Cam.Audio.Pause();
            AudioListener.pause = mute;
            AudioListener.volume = 0;
        } else
        {
            AudioListener.pause = mute;
            AudioListener.volume = userVolume;
            CameraController.Instance.Cam.Audio.UnPause();
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

    /// <summary>
    /// Populates the audioLibrary with audiodata
    /// </summary>
    /// <param name="audioList">Array of AudioData to fill the library with</param>
    public void FillAudioLibrary(AudioData[] audioList)
    {
        foreach(AudioData ad in audioList)
        {
            if(AudioLibrary.ContainsKey(ad.scene))
            {
                AudioLibrary[ad.scene].Add(ad);
            } else
            {
                List<AudioData> storage = new List<AudioData>();
                AudioLibrary.Add(ad.scene, storage);
            }
        }
    }
}
