using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct AudioData
{
    public int scene;
    public AudioClip clip;
    public PlayCase playCase;
}

public enum PlayCase
{
    Menu, Travel, Battle, Special
}

public class AudioHelper : MonoBehaviour
{
    [SerializeField]
    private AudioClip dimChime;
    [SerializeField]
    private AudioClip dim2NoteChime;
    [SerializeField]
    private AudioClip introlessLevelSong;

    public AudioClip DIM_CHIME { get { return dimChime; } }
    public AudioClip DIM_2NOTE_CHIME { get { return dim2NoteChime; } }
    public AudioClip INTROLESS_LEVEL_SONG { get { return introlessLevelSong; } }

    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(gameObject);

        AudioController.Instance.LinkAudioObject(this);
    }

    /// <summary>
    /// Change the intro track to the looping track
    /// </summary>
    private void TrackChange(AudioClip newTrack)
    {
        CameraController.Instance.Cam.GetComponent<AudioSource>().Stop();
        CameraController.Instance.Cam.GetComponent<AudioSource>().clip = newTrack;
        Debug.Log(CameraController.Instance.Cam.GetComponent<AudioSource>().clip);
        CameraController.Instance.Cam.GetComponent<AudioSource>().loop = true;
        CameraController.Instance.Cam.GetComponent<AudioSource>().Play();
    }

    /// <summary>
    /// Cancels invocation
    /// </summary>
    public void UnInvoke()
    {
        CancelInvoke();
    }
}
