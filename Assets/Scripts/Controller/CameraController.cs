using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class CameraController : Controller
{
    private GameObject gtView;

    private Vector2 cameraStart;

    public Camera Cam { get; private set; }
    public static CameraController Instance { get; private set; } = new CameraController();

    private CameraController()
    {
        if(Instance != null)
        {
            return;
        }

        Instance = this;
        title = GameTypeTitle.CAMERA;

    }

    protected override void SceneLoaded(Scene s, LoadSceneMode lsm)
    {
        CameraStart();
        Cam = GameObject.FindObjectOfType<Camera>();
    }

    /// <summary>
    /// Initialize the player view to the determined starting camera
    /// </summary>
    public void CameraStart()
    {
       
    }

    /// <summary>
    /// Send out a raycast to see what was clicked, then check if it has an OnClick function and execute it
    /// </summary>
    private void Clicked()
    {
        RaycastHit2D hit;
        hit = Physics2D.Raycast(Cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if(hit.collider.GetComponent<Clickable>() != null)
            {
                hit.collider.GetComponent<Clickable>().Clicked();
            }
        }
    }
}
