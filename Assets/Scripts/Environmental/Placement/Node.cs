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
    private Color startColor;
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
            this.preview = Instantiate(((Blueprint)BuildController.Instance.ToBuild).prefab);
            this.preview.transform.SetParent(this.gameObject.transform);
            this.preview.transform.localPosition = new Vector2(0, 0);

            this.preview.GetComponent<SpriteRenderer>().color = new Color(preview.GetComponent<SpriteRenderer>().color.r, preview.GetComponent<SpriteRenderer>().color.g, preview.GetComponent<SpriteRenderer>().color.b, .5f);

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
            Destroy(this.preview.gameObject);
            rend.color = startColor;
        }
    }

    private void Start()
    {
        rend = GetComponent<SpriteRenderer>();
        startColor = rend.color;
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
    void BuildItem(Blueprint b)
    {
        if (this.preview == null)
        {
            return;
        }

        GameObject _heldObject = Instantiate(b.prefab) as GameObject;
        heldObj = _heldObject;
        heldObj.GetComponent<SpriteRenderer>().sprite = preview.GetComponent<SpriteRenderer>().sprite;
        heldObj.transform.SetParent(gameObject.transform);
        heldObj.transform.SetPositionAndRotation(preview.transform.position, preview.transform.rotation);

        gameObject.GetComponent<SpriteRenderer>().color = startColor;

        objBlueprint = b;
        Destroy(preview);
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
}
