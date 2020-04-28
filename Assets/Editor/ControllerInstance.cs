using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ControllerInstance : MonoBehaviour
{
    //Controllers
    [MenuItem("GameObject/Controller/Level Controller", priority = 0)]
    public static void AddLevelController()
    {
        Create("Controller/LevelController", "LevelController");
    }

    [MenuItem("GameObject/Controller/Audio Controller", priority = 1)]
    public static void AddAudioController()
    {
        Create("Controller/AudioController", "AudioController");
    }

    [MenuItem("GameObject/Controller/Camera Controller", priority = 2)]
    public static void AddCameraController()
    {
        Create("Controller/CameraController", "CameraController");
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
