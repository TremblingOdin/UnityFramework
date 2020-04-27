
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Basic functional class to be used during runtime
/// A easy way to check if an object is clickable and to run it's selected Click function
/// </summary>
public class Clickable : MonoBehaviour
{
    [Tooltip("The function to be executed when clicked")]
    [SerializeField]
    private UnityEvent clicked;

    public void Clicked()
    {
        clicked.Invoke();
    }
}