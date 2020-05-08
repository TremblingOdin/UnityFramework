using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasePlatformer2D : Movement
{
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private bool canJump;
    [SerializeField]
    private bool canSwim;


    private bool isFalling;
    public bool IsGrounded { get { return isGrounded; } }
    public bool CanJump { get { return canJump; } }
    public bool CanSwim { get { return canSwim; } }

    [SerializeField]
    private int strokeTime;
    private int strokeCounter;

    [SerializeField]
    private float swimDampen;
    public float SwimDampen { get { return swimDampen; } set { swimDampen = value; } }

    [SerializeField]
    private float jumpForce;
    public float JumpForce { get { return jumpForce; } }

    [SerializeField]
    private string groundingTag;
    [SerializeField]
    private string waterTag;

    [SerializeField]
    private string walkForwardAnimationID;

    protected override void Awake()
    {
        base.Awake();
        movementType = MovementType.PLATFORMER;
        movement.Add(Player.UserInput.MOVELEFT, null);
        movement.Add(Player.UserInput.MOVERIGHT, null);
        movement.Add(Player.UserInput.JUMP, null);
        isGrounded = true;
        canJump = true;
        isFalling = false;

        //Player Object will function similar to singleton
        DontDestroyOnLoad(gameObject);
    }

    protected override void OnCollisionEnter2D(Collision2D other)
    {
        //If the character landed on something (the y position is below the character
        //and they were previously falling) then restore the jump
        if(other.transform.position.y < transform.position.y && isFalling)
        {
            canJump = true;
            isFalling = false;
        }

        //Check if the character landed on the ground then restore everything
        if (other.collider.tag == groundingTag)
        {
            isGrounded = true;
            canJump = true;
            isFalling = false;
        }
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == waterTag)
        {
            if(other.gameObject.GetComponent<Liquid2D>().InstaDeath)
            {
                //TODO: kill character
            }

            strokeCounter = strokeTime;
            Debug.Log(GetComponent<Rigidbody2D>().velocity);
            GetComponent<Rigidbody2D>().velocity *= .2f;

            Debug.Log(GetComponent<Rigidbody2D>().velocity);

            canSwim = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == waterTag)
        {
            strokeCounter = strokeTime;
            canSwim = false;
        }
    }

    //Fixed Update for movement for consistency
    protected void FixedUpdate()
    {
        if (LevelController.Instance.Paused)
        {
            return;
        }

        if(canSwim)
        {
            UnderWater();
        } else
        {
            Platforming();
        }
    }

    /// <summary>
    /// Handle the movement for when the character is underwater
    /// </summary>
    private void UnderWater()
    {

        Vector2 movementVector = Vector2.zero;

        foreach(Player.UserInput ui in movement.Keys)
        {
            if(movement[ui] != null && Input.GetKey((KeyCode)movement[ui]))
            {
                switch(ui)
                {
                    case Player.UserInput.MOVERIGHT:
                        if (canMove)
                            movementVector += Vector2.right;
                        break;
                    case Player.UserInput.MOVELEFT:
                        if (canMove)
                            movementVector += Vector2.left;
                        break;
                    case Player.UserInput.JUMP:
                        if (canMove)
                            movementVector += Vector2.up * 2;
                        break;
                    default:
                        Debug.Log("[BasePlatformer2D] Unhandled movement option: " + System.Enum.GetName(typeof(Player.UserInput), ui));
                        break;
                }
            }
        }

        if (movementVector != Vector2.zero && strokeCounter++ >= strokeTime)
        {
            strokeCounter = 0;
            GetComponent<Rigidbody2D>().AddForce(movementVector * swimDampen, ForceMode2D.Impulse);
        }
    }

    /// <summary>
    /// Handle regular platforming movement for the character
    /// </summary>
    private void Platforming()
{
        Vector2 movementVector = new Vector2(0, 0);
        Vector2 jumpVector = new Vector2(0, 0);

        foreach (Player.UserInput ui in movement.Keys)
        {
            if (movement[ui] != null && Input.GetKey((KeyCode)movement[ui]))
            {
                switch (ui)
                {
                    case Player.UserInput.MOVERIGHT:
                        if (canMove)
                            movementVector += Vector2.right;
                        break;
                    case Player.UserInput.MOVELEFT:
                        if (canMove)
                            movementVector += Vector2.left;
                        break;
                    case Player.UserInput.JUMP:
                        if (canJump)
                            jumpVector += Vector2.up;
                        break;
                    default:
                        Debug.Log("[BasePlatformer2D] Unhandled movement option: " + System.Enum.GetName(typeof(Player.UserInput), ui));
                        break;
                }
            }
        }

        if (movementVector != Vector2.zero)
        {
            GetComponent<Rigidbody2D>().velocity = movementVector * speed + new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
        } else
        {
            GetComponent<Rigidbody2D>().velocity = 
                    new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }


        if (jumpVector == Vector2.up)
        {
            GetComponent<Rigidbody2D>().AddForce(jumpVector * jumpForce);
            canJump = false;
        }


        if (GetComponent<Rigidbody2D>().velocity.y < 0f)
        {
            isFalling = true;
        }
        else
        {
            isFalling = false;
        }
    }

    /// <summary>
    /// Loads the movementSettings provided into a movement library
    /// It will look for: MOVELEFT, MOVERIGHT, JUMP
    /// </summary>
    /// <param name="movementSettings">The movement settings to use in update</param>
    /// <returns>What type of movement this script is configured for</returns>
    public override MovementType LoadMovement(Dictionary<KeyCode, Player.UserInput> movementSettings)
    {
        foreach(KeyCode key in movementSettings.Keys)
        {
            movement[movementSettings[key]] = key;
        }

        return movementType;
    }
}
