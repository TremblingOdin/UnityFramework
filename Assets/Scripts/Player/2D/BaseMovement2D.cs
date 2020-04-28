using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseMovement2D : Movement
{

    protected override void Awake()
    {
        base.Awake();
        movementType = MovementType.TOPDOWN;
        movement.Add(Player.UserInput.MOVEDOWN, null);
        movement.Add(Player.UserInput.MOVELEFT, null);
        movement.Add(Player.UserInput.MOVERIGHT, null);
        movement.Add(Player.UserInput.MOVEUP, null);
    }

    //Fixed Update for movement for consistency
    protected void FixedUpdate()
    {
        if(onPlayer)
        {
            Vector2 movementVector = new Vector2(0, 0);

            foreach (Player.UserInput ui in movement.Keys) {
                //Check if nulled, if not cast to a non-nullable version of itself
                if (movement[ui] != null && Input.GetKey((KeyCode)movement[ui]))
                {
                    switch(ui)
                    {
                        case Player.UserInput.MOVEDOWN:
                            if (canMove)
                                movementVector += Vector2.down;
                            break;
                        case Player.UserInput.MOVELEFT:
                            if (canMove)
                                movementVector += Vector2.left;
                            break;
                        case Player.UserInput.MOVERIGHT:
                            if (canMove)
                                movementVector += Vector2.right;
                            break;
                        case Player.UserInput.MOVEUP:
                            if (canMove)
                                movementVector += Vector2.up;
                            break;
                        default:
                            Debug.Log("[BaseMovement2D] Unhandled movement option: " + System.Enum.GetName(typeof(Player.UserInput), ui));
                            break;
                    }
                }
            }


            GetComponent<Rigidbody2D>().velocity = movementVector * speed;
        } else
        {
            //TODO: create some AI movement behaviour
        }
    }

    /// <summary>
    /// Loads the movementSettings provided into a movement library 
    /// It will look for: MOVEDOWN, MOVELEFT, MOVERIGHT, MOVEDOWN
    /// </summary>
    /// <param name="movementSettings">The movement settings to use in update</param>
    /// <returns>What type of movement this script is configured for</returns>
    public override MovementType LoadMovement(Dictionary<KeyCode, Player.UserInput> movementSettings)
    {
        foreach(KeyCode key in movementSettings.Keys)
        {
            //Remember movement's values are movementSettings' keys
            if(movement.ContainsKey(movementSettings[key]))
            {
                movement[movementSettings[key]] = key;
            }
        }

        return movementType;
    }
}
