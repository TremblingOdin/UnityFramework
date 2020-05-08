using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Node : MonoBehaviour
{
    /// <summary>
    /// Node that comes before it in Path
    /// </summary>
    public Node Parent { get; set; }

    /// <summary>
    /// F value PATH CALCULATION ONLY
    /// </summary>
    public int F { get; set; }

    /// <summary>
    /// G value PATH CALCULATION ONLY
    /// </summary>
    public int G { get; set; }

    /// <summary>
    /// H value PATH CALCULATION ONLY
    /// </summary>
    public int H { get; set; }

    //Position and pathfinding
    private Vector2 position; //This should be a Vector2Int in a grid system
    private Node discoveredBy;
    private bool isDiscovered;

    /// <summary>
    /// The tile that discovered this one, to generate breadcrumbs for pathfinding
    /// </summary>
    public Node DiscoveredBy
    {
        get { return discoveredBy; }
        set { discoveredBy = value; }
    }

    /// <summary>
    /// If the tile has been discovered during pathfinding
    /// </summary>
    public virtual bool IsDiscovered
    {
        get { return isDiscovered; }
        set { isDiscovered = value; }
    }

    //For games where you can build on a node like Towerdefense
    [SerializeField]
    private bool isBuildable;
    [SerializeField]
    private Color hoverColor;
    [SerializeField]
    private Color startColor;
    [SerializeField]
    private SpriteRenderer rend;
    private GameObject preview;

    public Color HoverColor
    {
        get { return hoverColor; }
    }

    public bool IsBuildable
    {
        get { return isBuildable; }
        set { isBuildable = value; }
    }


    [SerializeField]
    private GameObject heldObj;
    [SerializeField]
    private Blueprint objBlueprint;

    public GameObject HeldObj {
        get { return heldObj; }
        set { heldObj = value; } 
    }
    public Blueprint ObjBlueprint
    {
        get { return objBlueprint; }
        set { objBlueprint = value; }
    }

    public Vector2Int ArrayCoord { get; set; }

    /// <summary>
    /// If buildable and there is a selected build sprite, show the hover color
    /// </summary>
    void OnMouseEnter()
    {
        if (!BuildController.Instance.CanBuild || !this.isBuildable || this.heldObj != null)
        {
            return;
        }
        else
        {
            preview = Instantiate((BuildController.Instance.ToBuild).prefab);
            preview.transform.SetParent(gameObject.transform);
            preview.transform.localPosition = new Vector2(0, 0);

            preview.GetComponent<SpriteRenderer>().color = new Color(preview.GetComponent<SpriteRenderer>().color.r, preview.GetComponent<SpriteRenderer>().color.g, preview.GetComponent<SpriteRenderer>().color.b, .5f);

            if (preview.GetComponent<Buildable>().Rotateable) {
                EventService.Instance.RegisterEventHandler(EventType.RotateLeft, RotateCounterClockwise);
                EventService.Instance.RegisterEventHandler(EventType.RotateRight, RotateClockwise);
            }
        }

        rend.color = hoverColor;
    }

    /// <summary>
    /// Changes the selected sprite render color back to default
    /// </summary>
    private void OnMouseExit()
    {
        if(this.preview != null)
        {
            EventService.Instance.ClearEvents(EventType.RotateLeft);
            EventService.Instance.ClearEvents(EventType.RotateRight);
            Destroy(this.preview.gameObject);
            rend.color = startColor;
        }
    }

    /// <summary>
    /// On click build
    /// </summary>
    public void OnMouseDown()
    {
        if(BuildController.Instance.CanBuild && preview != null
            && !LevelController.Instance.Paused)
        {
            if(BuildItem(BuildController.Instance.ToBuild))
            {
                BuildController.Instance.Built();
            }
        }
    }

    private void Awake()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
    }

    /// <summary>
    /// Rotates the preview item counterclockwise
    /// </summary>
    private void RotateCounterClockwise()
    {
        preview.transform.Rotate(0f, 0f, -90f);
        BuildController.Instance.StoredRotation = preview.transform.rotation;
    }

    /// <summary>
    /// Rotate the preview item clockwise
    /// </summary>
    private void RotateClockwise()
    {
        preview.transform.Rotate(0f, 0f, 90f);
        BuildController.Instance.StoredRotation = preview.transform.rotation;
    }

    /// <summary>
    /// For specific nodes to aid in the clicking functionality
    /// Will not execute unless there is also a Clickable script
    /// </summary>
    public void Clicked()
    {
        if (BuildController.Instance.CanBuild)
        {
            BuildItem(BuildController.Instance.ToBuild);
        }
    }

    /// <summary>
    /// Designates the held item being built
    /// </summary>
    /// <param name="b">Blueprint of item to build</param>
    bool BuildItem(Blueprint b)
    {
        if (preview == null)
        {
            return false;
        }

        GameObject _heldObject = Instantiate(b.prefab) as GameObject;
        heldObj = _heldObject;

        heldObj.GetComponent<SpriteRenderer>().sprite = preview.GetComponent<SpriteRenderer>().sprite;
        heldObj.transform.SetParent(gameObject.transform, false);
        heldObj.transform.SetPositionAndRotation(preview.transform.position, preview.transform.rotation);

        if(!NeighborNodes(heldObj))
        {
            Destroy(heldObj.gameObject);
            heldObj = null;
            return false;
        }

        gameObject.GetComponent<SpriteRenderer>().color = startColor;

        objBlueprint = b;

        EventService.Instance.ClearEvents(EventType.RotateLeft);
        EventService.Instance.ClearEvents(EventType.RotateRight);

        Destroy(preview);

        return true;
    }

    /// <summary>
    /// Removes the held item that was built
    /// </summary>
    public void RemoveItem()
    {
        Destroy(heldObj);
        objBlueprint = null;
    }

    /// <summary>
    /// Gets the coordinates of the tile
    /// </summary>
    /// <returns>Coordinate of tile</returns>
    public virtual Vector2 GetCoordinate()
    {
        return position;
    }

    /// <summary>
    /// This function is necessary because the Instantiate() method (in BuildManager) requires a Vector3
    /// </summary>
    /// <returns>Coordinate of tile including Z axis (0) (TODO: Z-value should NOT be hardcoded)</returns>
    public virtual Vector3 GetCoordinate3D()
    {
        return new Vector3((int)gameObject.transform.position.x, 
                            (int)gameObject.transform.position.y, 
                            (int)gameObject.transform.position.z);
    }

    /// <summary>
    /// Returns if this node is occupied by a construct
    /// </summary>
    /// <returns></returns>
    public bool HasHeldObj()
    {
        if (this.heldObj != null)
        {
            return true;
        }

        return false;
    }

    /// <summary>
    /// Based on the Amount of Nodes the passed build object takes up, check neighbors and see if it can be built
    /// </summary>
    /// <param name="builder"></param>
    private bool NeighborNodes(GameObject builder)
    {
        int angle = (int)Mathf.Floor(builder.transform.rotation.eulerAngles.z);
        if (angle < 0)
        {
            angle = 360 + angle;
        }

        //TODO: fill as needed
        switch (builder.GetComponent<Buildable>().NodeCount)
        {
            default:
                break;
        }

        return true;
    }
}
