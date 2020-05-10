using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class BasePlatformer2D : Movement
{
    [SerializeField]
    private bool isGrounded;
    [SerializeField]
    private float groundRadius = 0.1f;
    [SerializeField]
    private float wallRadius = 0.1f;
    [SerializeField]
    private bool touchingWall;

    [SerializeField]
    private LayerMask whatIsGround;
    [SerializeField]
    private LayerMask whatIsWall;

    [SerializeField]
    private Transform groundCheck;
    [SerializeField]
    private Transform[] wallCheck;

    [SerializeField]
    private bool canJump;

    public bool TouchingWall { get { return touchingWall; } }
    public bool IsGrounded { get { return isGrounded; } }
    public bool CanJump { get { return canJump; } set { canJump = value; } }

    [SerializeField]
    private float jumpForce;
    public float JumpForce { get { return jumpForce; } }

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

        EventService.Instance.RegisterEssential(GetType());
        EventService.Instance.RegisterEventHandler(EventType.StartSwim, DisableMovement);
        EventService.Instance.RegisterEventHandler(EventType.StopSwim, StartMovement);

        //Player Object will function similar to singleton
        DontDestroyOnLoad(gameObject);
    }

    //Fixed Update for movement for consistency
    protected void Update()
    {
        if (LevelController.Instance.Paused)
        {
            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);
        if(isGrounded)
        {
            canJump = true;
        }

        WallChecker();

        Platforming();        
    }

    /// <summary>
    /// Checks if the player is touching a wall and assigns the wall check boolean
    /// </summary>
    private void WallChecker()
    {
        if(GetComponent<WallCheck2D>() == null)
        {
            return;
        }

        GetComponent<WallCheck2D>().RightWall = false;

        //We need to check both sides of the player so you check all transforms in wall check
        //this means use a temp variable to make sure that you aren't using a past state
        if (wallCheck.Length > 0)
        {
            bool walltouch = false;
            foreach (Transform t in wallCheck)
            {
                if (walltouch) break;
                walltouch = Physics2D.OverlapCircle(t.position, wallRadius, whatIsWall);
                if(t.localPosition.x > 0)
                {
                    GetComponent<WallCheck2D>().RightWall = true;
                }
            }

            touchingWall = walltouch;
        }
        else
        {
            touchingWall = false;
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
            //I want jump to be more controlled by the player
            if(movement[ui] != null && ui == Player.UserInput.JUMP
                && Input.GetKeyDown((KeyCode)movement[ui]))
            {
                Debug.Log(ui);

                if (touchingWall && FaceRight && CanJump)
                {
                    jumpVector = Vector2.up + Vector2.right * 2;
                    canJump = false;
                }
                else if (touchingWall && !FaceRight && CanJump)
                {
                    jumpVector = Vector2.up + Vector2.left * 2;
                    canJump = false;
                }
                else if (CanJump)
                {
                    jumpVector = Vector2.up;
                    canJump = false;
                }
            }

            if (movement[ui] != null && Input.GetKey((KeyCode)movement[ui]))
            {
                switch (ui)
                {
                    case Player.UserInput.MOVERIGHT:
                        if (canMove && HorizontalMove)
                        {
                            movementVector += Vector2.right;
                            FaceRight = true;
                        }
                        break;
                    case Player.UserInput.MOVELEFT:
                        if (canMove && HorizontalMove)
                        {
                            movementVector += Vector2.left;
                            FaceRight = false;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        if (movementVector != Vector2.zero)
        {
            GetComponent<Rigidbody2D>().velocity = movementVector * speed + new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
        } else if (HorizontalMove && VerticalMove)
        {
            GetComponent<Rigidbody2D>().velocity = 
                    new Vector2(0, GetComponent<Rigidbody2D>().velocity.y);
        }


        if (jumpVector != Vector2.zero)
        {
            GetComponent<Rigidbody2D>().AddForce(jumpVector * jumpForce);
            canJump = false;
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
