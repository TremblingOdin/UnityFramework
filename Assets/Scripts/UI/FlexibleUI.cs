 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[ExecuteInEditMode()]
public class FlexibleUI : MonoBehaviour
{
    public FlexibleUIData skinData;
    public bool dontChange;
    private bool hasChanged = false;

    public virtual void OnSkinUI()
    {
        if (!hasChanged || dontChange)
        {
            return;
        } else
        {
            hasChanged = false;
        }
    }


    public virtual void OnValidate()
    {
        hasChanged = true;
    }

    public virtual void Awake()
    {
        OnSkinUI();
    }


}

[CustomEditor(typeof(FlexibleUI), true)]
[DisallowMultipleComponent]
public class FlexibleUIEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Refresh Skins"))
        {
            RefreshSkins();
        }
    }

    /// <summary>
    /// Go through all the Flexible UI's and refresh their skins
    /// </summary>
    private void RefreshSkins()
    {
        FlexibleUI[] components = Resources.FindObjectsOfTypeAll<FlexibleUI>();

        foreach(FlexibleUI fu in components)
        {
            fu.OnSkinUI();
        }
    }
}
