using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public enum UserInput
    {
        DASH, JUMP, MOVEDOWN, MOVELEFT, MOVERIGHT, MOVEUP, ATTACK, INTERACT
    }


    public const GameTypeTitle title = GameTypeTitle.PLAYER;

    [SerializeField]
    private Movement movementSystem;
    private Movement.MovementType movement;

    public float DefaultGravity { get; set; }
    public float DefaultAngularDrag { get; set; }

    [SerializeField]
    private Movement swimming;
    public bool CanSwim { get; private set; }

    [SerializeField]
    private SpecialsManager platformerSpecials;

    protected Dictionary<KeyCode, UserInput> interact;

    protected virtual void Awake()
    {
        GameController.Instance.RegisterType(this, title, true);
        DefaultGravity = GetComponent<Rigidbody2D>().gravityScale;
        DefaultAngularDrag = GetComponent<Rigidbody2D>().angularDrag;

        if(swimming != null)
        {
            CanSwim = true;
        }

        DontDestroyOnLoad(gameObject);
    }

    protected virtual void Start()
    {
        interact = GameController.Instance.PlayControl;
        movement = movementSystem.LoadMovement(interact);

        if (swimming != null)
        {
            swimming.LoadMovement(interact);
        }

        if(platformerSpecials != null)
        {
            platformerSpecials.LoadKeys(interact);
        }

        if(movement == Movement.MovementType.PLATFORMER && !interact.ContainsValue(UserInput.JUMP)
            || movement == Movement.MovementType.TOPDOWN && !(interact.ContainsValue(UserInput.MOVEUP) || interact.ContainsValue(UserInput.MOVEDOWN)))
        {
            Debug.LogWarning("The Movement options don't agree with the movement type, please double check everything");
        }
    }



}
