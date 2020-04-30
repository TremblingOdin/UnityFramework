using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildController : MonoBehaviour
{
    public static BuildController Instance { get; private set; }
    public const GameTypeTitle title = GameTypeTitle.BUILD;

    private Blueprint toBuild;
    public Blueprint ToBuild { get { return toBuild; } }

    public bool CanBuild { get; set; }

    void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("More than one [BuildManager] attempted to run");
            return;
        }

        Instance = this;
        GameController.Instance.RegisterType(this, title, false);
    }

    /// <summary>
    /// Pick an Item to checkout
    /// Adjust for each game to display in UI
    /// </summary>
    /// <param name="b">Item to examine</param>
    public void SelectItem(Blueprint b)
    {
        toBuild = b;
    }

    /// <summary>
    /// Unpick an item
    /// </summary>
    public void DeselectItem()
    {
        toBuild = null;
    }
}
