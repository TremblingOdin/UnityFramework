using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class HelperInstance : MonoBehaviour
{
    //Controllers
    [MenuItem("GameObject/Helpers/Keyboard To Event Helper", priority = 0)]
    public static void AddLevelController()
    {
        Create("Helpers/KeyboardHelper", "Keh");
    }

    [MenuItem("GameObject/Helpers/Audio Helper", priority = 1)]
    public static void AddAudioController()
    {
        Create("Helpers/AudioHelper", "AH!");
    }

    static GameObject clickedObject;

    private static GameObject Create(string objectPath, string objectName)
    {
        GameObject instance = Instantiate(Resources.Load<GameObject>(objectPath));
        instance.name = objectName;
        clickedObject = Selection.activeObject as GameObject;
        if (clickedObject != null)
        {
            instance.transform.SetParent(clickedObject.transform, false);
        }

        return instance;
    }
}
