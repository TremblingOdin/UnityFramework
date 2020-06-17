using UnityEngine;

public class CameraHelper : MonoBehaviour
{
    [SerializeField]
    private Camera realCam;
    [SerializeField]
    private AudioSource audioSource;

    public Camera RealCam { get { return realCam; } set { realCam = value; } }
    public AudioSource Audio { get { return audioSource; } set { audioSource = value; } }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
