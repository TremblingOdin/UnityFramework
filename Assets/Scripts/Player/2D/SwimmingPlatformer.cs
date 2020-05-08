using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwimmingPlatformer : Movement
{
    [SerializeField]
    private bool isSwimming;
    public bool IsSwimming { get { return isSwimming; } }

    //I really want a floaty weightless feeling for the swimming so this helps
    [SerializeField]
    private int floatFrames;
    private int floatCount;

    [SerializeField]
    private string waterTag;

    [SerializeField]
    private float swimDampen;
    public float SwimDampen { get { return swimDampen; } set { swimDampen = value; } }

    protected override void Awake()
    {
        base.Awake();
        movementType = MovementType.SWIMMING;

        floatCount = 0;
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == waterTag)
        {
            if (other.gameObject.GetComponent<Liquid2D>().InstaDeath)
            {
                //TODO: kill character
            }

            GetComponent<Rigidbody2D>().velocity *= .1f;

            EventService.Instance.HandleEvents(EventType.StartSwim);

            isSwimming = true;
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == waterTag)
        {
            isSwimming = false;

            EventService.Instance.HandleEvents(EventType.StopSwim);
        }
    }

    protected void Update()
    {
        if(isSwimming)
        {
            UnderWater();
        }
    }

    /// <summary>
    /// Handle the movement for when the character is underwater
    /// </summary>
    private void UnderWater()
    {

        Vector2 movementVector = Vector2.zero;

        foreach (Player.UserInput ui in movement.Keys)
        {
            if (movement[ui] != null
                && Input.GetKeyDown((KeyCode)movement[ui]))
            {
                switch (ui)
                {
                    case Player.UserInput.MOVERIGHT:
                        if (canMove)
                            GetComponent<Rigidbody2D>().AddForce(Vector2.right * speed, ForceMode2D.Impulse);
                        break;
                    case Player.UserInput.MOVELEFT:
                        if (canMove)
                            GetComponent<Rigidbody2D>().AddForce(Vector2.left * speed, ForceMode2D.Impulse);
                        break;
                    //I really want this to be floaty and this is what worked
                    case Player.UserInput.JUMP:
                        if (canMove)
                        {
                            GetComponent<Rigidbody2D>().AddForce(Vector2.up * 3f * speed, ForceMode2D.Impulse);
                            floatCount = 0;
                        }
                        break;
                    default:
                        break;
                }
            }
        }

        if(GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x,0);
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
        foreach (KeyCode key in movementSettings.Keys)
        {
            movement[movementSettings[key]] = key;
        }

        return movementType;
    }
}
