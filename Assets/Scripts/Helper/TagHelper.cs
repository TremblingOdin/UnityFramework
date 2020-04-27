using System.Collections.Generic;
using UnityEngine;

public static class TagHelper
{
    //Used like Turret t = parentObject.FindComponentInChildWithTag<Turret>("T1000");
    /// <summary>
    /// Method used to locate Child with provided tag in the provided GameObject
    /// Always returns first Child found
    /// </summary>
    /// <param name="parent">The GameObject calling the function.</param>
    /// <param name="tag">Tag associated with the child GameObject that is sought.</param>
    /// <returns>The child GameObject with the provided tag found, or null.</returns>
    public static GameObject FindChildWithTag(this GameObject parent, string tag)
    {
        if (parent == null || string.IsNullOrEmpty(tag) == true)
        {
            Debug.LogError("[TagHelper] parent of " + parent + " or tag " + tag + " need to be double checked");
            throw new System.ArgumentNullException();
        }

        Transform t = parent.transform;
        foreach (Transform tr in t)
        {
            if (tr.tag == tag)
            {
                return tr.gameObject;
            }
        }
        return null;
    }

    /// <summary>
    /// Returns an array of children with the provided tag
    /// </summary>
    /// <param name="parent">The GameObject calling the function.</param>
    /// <param name="tag">That tag of the Children GameObjects sought.</param>
    /// <returns>A List of Children GameObjects with the provided tag.</returns>
    public static List<GameObject> FindChildrenWithTag(this GameObject parent, string tag)
    {
        if (parent == null || string.IsNullOrEmpty(tag))
        {
            Debug.LogError("[TagHelper] parent of " + parent + " or tag " + tag + " need to be double checked");
            throw new System.ArgumentNullException();
        }

        //It needs to initialize an empty list to avoid running into issues if there is no child with tag
        List<GameObject> objectList = new List<GameObject>();

        for (int i = 0; i < parent.transform.childCount; i++)
        {
            if (parent.transform.GetChild(i).tag == tag)
            {
                objectList.Add(parent.transform.GetChild(i).gameObject);
            }
        }

        return objectList;
    }

    /// <summary>
    /// This finds the first component with the given tag amongst the children of the GameObject calling the function.
    /// This is intended to be used with objects where it is guaranteed that they only have one child with a given tag and target component.
    /// Don't make an equivalent for "Children" because that will be a confusing data set to work with
    /// </summary>
    /// <typeparam name="T">Type or Class of component desired.</typeparam>
    /// <param name="parent">GameObject calling the function.</param>
    /// <param name="tag">Tag of the child object that the component is expected to be in.</param>
    /// <returns>The Component of the Child Object with Tag</returns>
    public static T FindChildComponentWithTag<T>(this GameObject parent, string tag) where T : Component
    {
        if (parent == null || string.IsNullOrEmpty(tag))
        {
            Debug.LogError("[TagHelper] parent of " + parent + " or tag " + tag + " need to be double checked");
            throw new System.ArgumentNullException();
        }

        if (parent.FindChildrenWithTag(tag) == null)
        {
            return null;
        }

        foreach (GameObject obj in parent.FindChildrenWithTag(tag))
        {
            if (obj.GetComponent<T>() != null)
            {
                return obj.GetComponent<T>();
            }
        }

        return null;
    }

    /// <summary>
    /// Find the child of a child in the object with the provided name
    /// </summary>
    /// <param name="parent">GameObject calling the function</param>
    /// <param name="name">Name of the child in the Unity Editor to be sought</param>
    /// <returns>The Child of the Child with the provided name of the GameObject calling the function if found</returns>
    public static GameObject GetGrandChild(this GameObject parent, string name)
    {
        for (int i = 0; i< parent.transform.childCount; i++)
        {
            for(int j =0; j < parent.transform.GetChild(i).childCount; j++)
            {
                if(parent.transform.GetChild(i).transform.GetChild(j).name == name)
                {
                    return parent.transform.GetChild(i).transform.GetChild(j).gameObject;
                }
            }
        }

        return null;
    }
}
