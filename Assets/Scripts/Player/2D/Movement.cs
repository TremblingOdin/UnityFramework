using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public enum MovementType
    {
        PLATFORMER, TOPDOWN, NONE
    }

    protected bool onPlayer;

    [SerializeField]
    protected float speed;
    [SerializeField]
    protected bool canMove;

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

    //Flipping this because before it was "Oh we have the keycode let's read what it should be attached to"
    //But with movement we have a general idea of "Oh we know what we can do what's the keycode?"
    protected Dictionary<Player.UserInput, KeyCode?> movement;

    protected virtual void Awake()
    {
        movement = new Dictionary<Player.UserInput, KeyCode?>();
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
}