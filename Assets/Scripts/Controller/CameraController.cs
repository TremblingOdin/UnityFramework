using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraController : MonoBehaviour
{
    public const GameTypeTitle title = GameTypeTitle.CAMERA;


    [Tooltip("Game ToolTip HUD object")]
    [SerializeField]
    private GameObject gtView;

    private Vector2 cameraStart;


    [SerializeField]
    private Camera cam;

    public Camera Cam
    {
        get { return cam; }
    }


    private void Awake()
    {
        this.CameraStart();
        cameraStart = new Vector2(this.transform.position.x, this.transform.position.y);
        GameController.Instance.RegisterType(this, title, false);

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
        hit = Physics2D.Raycast(cam.ScreenToWorldPoint(Input.mousePosition), Vector2.zero);
        if (hit.collider != null)
        {
            if(hit.collider.GetComponent<Clickable>() != null)
            {
                hit.collider.GetComponent<Clickable>().Clicked();
            }
        }
    }
}
