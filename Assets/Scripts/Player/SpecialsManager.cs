using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum UpgradeType
{
    DASH, WALLHANG, JUMP
}

public class SpecialsManager : MonoBehaviour
{
    /// <summary>
    /// Upgrade to keep track of and if it's enabled
    /// </summary>
    private Dictionary<UpgradeType, Special> upgradeLibrary;

    [SerializeField]
    private Special[] specials;

    private Dictionary<KeyCode, UpgradeType> keyToUpgradeConversion;

    void Awake()
    {
        upgradeLibrary = new Dictionary<UpgradeType, Special>();
        keyToUpgradeConversion = new Dictionary<KeyCode, UpgradeType>();

        foreach(Special s in specials)
        {
            upgradeLibrary.Add(s.UT, s);
            s.enabled = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        foreach(KeyCode key in keyToUpgradeConversion.Keys)
        {
            if(Input.GetKey(key) && upgradeLibrary[keyToUpgradeConversion[key]].enabled)
            {
                upgradeLibrary[keyToUpgradeConversion[key]].PerformSpecial();
            }
        }
    }

    /// <summary>
    /// Allows the upgrade to execute
    /// </summary>
    /// <param name="newUpgrade"></param>
    public void EnableUpgrade(UpgradeType ut)
    {
        upgradeLibrary[ut].enabled = true; 
    }

    /// <summary>
    /// Loads the provided upgrade and key into the movement library
    /// So that the code may easily call the activation of the special
    /// </summary>
    /// <param name="upgrade">Upgrade to load</param>
    /// <param name="key">KeyCode to associate</param>
    public void LoadKeys(Dictionary<KeyCode, Player.UserInput> inputDict)
    {
        foreach(KeyCode key in inputDict.Keys)
        {
            if(((Player.UserInput?)inputDict[key]).InputToUpgrade() != null)
            {
                keyToUpgradeConversion.Add(key, (UpgradeType) ((Player.UserInput?)inputDict[key]).InputToUpgrade());
            }
        }
    }
}
