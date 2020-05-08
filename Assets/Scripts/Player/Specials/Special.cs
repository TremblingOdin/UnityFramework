using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Special : MonoBehaviour
{
    [SerializeField]
    protected UpgradeType ut;
    public UpgradeType UT { get { return ut; } }

    /// <summary>
    /// Performs the special for the script when called
    /// </summary>
    public virtual void PerformSpecial()
    {

    }
}
