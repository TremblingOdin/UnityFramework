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

    protected Dictionary<KeyCode, UserInput> interact;

    protected virtual void Awake()
    {
        GameController.Instance.RegisterType(this, title, true);
    }

    protected virtual void Start()
    {
        interact = GameController.Instance.PlayControl;
    }



}
