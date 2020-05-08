using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
