using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum MovementType
    {
        PLATFORMER, SWIMMING, TOPDOWN, NONE
    }

    protected bool onPlayer;

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected bool canMove;
    [SerializeField]
    protected bool horizontalMove;
    [SerializeField]
    protected bool verticalMove;

    protected MovementType movementType = MovementType.NONE;

    public float Speed
    {
        get { return speed; }
        set { speed = value; }
    }

    public bool CanMove
    {
        get { return canMove; }
        set { canMove = value; }
    }

    public bool HorizontalMove
    {
        get { return horizontalMove; }
    }

    public bool VerticalMove
    {
        get { return verticalMove; }
    }

    //Flipping this because before it was "Oh we have the keycode let's read what it should be attached to"
    //But with movement we have a general idea of "Oh we know what we can do what's the keycode?"
    protected Dictionary<Player.UserInput, KeyCode?> movement;

    public Dictionary<Player.UserInput, KeyCode?> MovementDictionary { get { return movement; } }

    protected virtual void Awake()
    {
        movement = new Dictionary<Player.UserInput, KeyCode?>();
        StartMovement();
    }

    protected virtual void Start()
    {
        if(GetComponent<Player>() != null)
        {
            onPlayer = true;
        }
        else
        {
            onPlayer = false;
        }
    }

    protected virtual void OnCollisionEnter2D(Collision2D other)
    {
        return;
    }

    /// <summary>
    /// Loads the movementSettings provided into a movement library 
    /// </summary>
    /// <param name="movementSettings">The movement settings to use in update</param>
    /// <returns>What type of movement this script is configured for</returns>
    public virtual MovementType LoadMovement(Dictionary<KeyCode, Player.UserInput> movementSettings)
    {
        return movementType;
    }

    /// <summary>
    /// Disables all movement
    /// </summary>
    public virtual void DisableMovement()
    {
        canMove = false;
        horizontalMove = false;
        verticalMove = false;
    }

    /// <summary>
    /// Feezes movement for the provided axis
    /// </summary>
    /// <param name="horizontal"></param>
    /// <param name="vertical"></param>
    public virtual void StopMovement(bool horizontal, bool vertical)
    {
        if (horizontal)
            horizontalMove = false;

        if (vertical)
            verticalMove = false;
    }

    /// <summary>
    /// Restarts movement for all axis
    /// </summary>
    public virtual void StartMovement()
    {
        horizontalMove = true;
        verticalMove = true;
        canMove = true;
    }
}