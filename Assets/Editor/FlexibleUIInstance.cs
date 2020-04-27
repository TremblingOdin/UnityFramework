using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class FlexibleUIInstance : Editor
{
    [MenuItem("GameObject/Flexible UI/Button", priority = 0)]
    public static void AddButton()
    {
        Create("Button");
    }

    [MenuItem("GameObject/Flexible UI/Dropdown", priority = 1)]
    public static void AddDropdown()
    {
        Create("Dropdown");
    }

    [MenuItem("GameObject/Flexible UI/Text", priority = 2)]
    public static void AddText()
    {
        Create("Text");
    }

    [MenuItem("GameObject/Flexible UI/Toggle", priority = 3)]
    public static void AddToggle()
    {
        Create("Toggle");
    }

    static GameObject clickedObject;

    private static GameObject Create(string objectName)
    {
        GameObject instance = Instantiate(Resources.Load<GameObject>(objectName));
        instance.name = objectName;
        clickedObject = Selection.activeObject as GameObject;
        if(clickedObject != null)
        {
            instance.transform.SetParent(clickedObject.transform, false);
        }

        return instance;
    }
}
