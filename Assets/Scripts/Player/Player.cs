using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum UserInput
    {
        JUMP, MOVEDOWN, MOVELEFT, MOVERIGHT, MOVEUP, ATTACK, INTERACT
    }


    public const GameTypeTitle title = GameTypeTitle.PLAYER;

    [SerializeField]
    private Movement movementSystem;
    private Movement.MovementType movement;

    protected Dictionary<KeyCode, UserInput> interact;

    protected virtual void Awake()
    {
        GameController.Instance.RegisterType(this, title, true);
    }

    protected virtual void Start()
    {
        interact = GameController.Instance.PlayControl;
        movement = movementSystem.LoadMovement(interact);

        if(movement == Movement.MovementType.TOPDOWN && interact.ContainsValue(UserInput.JUMP)
            || movement == Movement.MovementType.PLATFORMER && (interact.ContainsValue(UserInput.MOVEUP) || interact.ContainsValue(UserInput.MOVEDOWN)))
        {
            Debug.LogWarning("The Movement options don't agree with the movement type, please double check everything");
        }
    }



}
