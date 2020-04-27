using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public const GameTypeTitle title = GameTypeTitle.AUDIO;

    private bool mute = false;

    private float userVolume = 100;

    private AudioClip dimChime;
    private AudioClip dim2NoteChime;

    [SerializeField]
    private AudioClip introlessLevelSong;

    public AudioClip DIM_CHIME { get { return this.dimChime; } }
    public AudioClip DIM_2NOTE_CHIME { get { return this.dim2NoteChime; } }

    private void Awake()
    {
        GameController.Instance.RegisterType(this, title, false);

    }

    private void Start()
    {
        userVolume = (float)GameController.Instance.UserSettings[UserSetting.VOLUME];
        GameController.Instance.GetType<CameraController>(GameTypeTitle.CAMERA).gameObject.GetComponent<AudioSource>().Play();
        //Invoke("TrackChange", GameController.Instance.GetType<CameraController>(GameTypeTitle.CAMERA).gameObject.GetComponent<AudioSource>().clip.length);
    }

    /// <summary>
    /// Change the intro track to the looping track
    /// </summary>
    private void TrackChange()
    {
        GameController.Instance.GetType<CameraController>(GameTypeTitle.CAMERA).gameObject.GetComponent<AudioSource>().Stop();
        GameController.Instance.GetType<CameraController>(GameTypeTitle.CAMERA).gameObject.GetComponent<AudioSource>().clip = introlessLevelSong;
        Debug.Log(GameController.Instance.GetType<CameraController>(GameTypeTitle.CAMERA).gameObject.GetComponent<AudioSource>().clip);
        GameController.Instance.GetType<CameraController>(GameTypeTitle.CAMERA).gameObject.GetComponent<AudioSource>().loop = true;
        GameController.Instance.GetType<CameraController>(GameTypeTitle.CAMERA).gameObject.GetComponent<AudioSource>().Play();
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
}
