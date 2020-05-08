using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : Controller
{
    public static BuildController Instance { get; private set; } = new BuildController();
   
    public Blueprint ToBuild { get; private set; }
    public Buildable BuildItem { get; private set; }

    public GameObject UIObject { get; set; }

    public bool CanBuild { get; set; }

    public Color DisabledColor { get; set; }

    public Quaternion StoredRotation { get; set; }

    BuildController()
    {
        if(Instance != null)
        {
            Debug.LogError("More than one [BuildManager] attempted to run");
            return;
        }

        Instance = this;

        title = GameTypeTitle.BUILD;
        CanBuild = false;

    }

    /// <summary>
    /// Pick an Item to checkout
    /// Adjust for each game to display in UI
    /// </summary>
    /// <param name="b">Item to examine</param>
    public void SelectItem(Blueprint b)
    {
        ToBuild = b;
        BuildItem = b.prefab.GetComponent<Buildable>();
    }

    /// <summary>
    /// Unpick an item
    /// </summary>
    public void DeselectItem()
    {
        ToBuild = null;
        BuildItem = null;
    }

    /// <summary>
    /// If anything special needs to happen to the build controller after an object is built
    /// Put it in here
    /// </summary>
    public void Built()
    {

    }
}
