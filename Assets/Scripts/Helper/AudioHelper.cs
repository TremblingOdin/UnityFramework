using UnityEngine;

[System.Serializable]
public struct AudioData
{
    public int scene;
    public AudioClip clip;
    public PlayCase playCase;
}

public enum PlayCase
{
    Menu, Travel, Battle, Special, Intro, Default
}

public class AudioHelper : MonoBehaviour
{
    // Serialized Audio Clips for SFX and the public getters for them

    // Dictionary List to be assigned to audio controller
    private AudioData[] audioList;
    //Used in QueueTrackChange to hold the future audioclip
    private AudioClip queue;

    // Start is called before the first frame update
    void Awake()
    {
        if(AudioController.Instance.AudioObject != null && AudioController.Instance.AudioObject != this)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        AudioController.Instance.LinkAudioObject(this);
        AudioController.Instance.FillAudioLibrary(audioList);
    }

    /// <summary>
    /// Change the intro track to the looping track
    /// </summary>
    private void TrackChange(AudioClip newTrack)
    {
        CameraController.Instance.Cam.Audio.Stop();
        CameraController.Instance.Cam.Audio.clip = newTrack;
        CameraController.Instance.Cam.Audio.loop = true;
        CameraController.Instance.Cam.Audio.Play();
    }

    public void QueueTrackChange(AudioClip firstTrack, AudioClip nextTrack)
    {
        float length = firstTrack.length;
        queue = nextTrack;

        TrackChange(firstTrack);
        CameraController.Instance.Cam.Audio.loop = false;

        Invoke("PlayNextClip", length);
    }

    /// <summary>
    /// Cancels invocation
    /// </summary>
    public void UnInvoke()
    {
        CancelInvoke();
    }

    /// <summary>
    /// Sets the playing clip to what's queued and set's it to loop
    /// </summary>
    public void PlayNextClip()
    {
        CameraController.Instance.Cam.Audio.Stop();
        CameraController.Instance.Cam.Audio.clip = queue;
        CameraController.Instance.Cam.Audio.loop = true;
        CameraController.Instance.Cam.Audio.Play();
    }
}
