using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dash : Special
{
    [SerializeField]
    private int damage;
    [SerializeField]
    private float dashSpeed;
    [SerializeField]
    private int invincibilityFrames;
    [SerializeField]
    private int dashFrames;
    [SerializeField]
    private int dirDecisionFrames;
    [SerializeField]
    private bool dashing;
    [SerializeField]
    private bool dirSet;
    [SerializeField]
    private Vector2 dashDir;

    private int invincibilityFramesCount;
    private int dirDecisionFramesCount;
    private int dashFramesCount;

    public bool Dashing { get { return dashing; } }

    void Awake()
    {
        ut = UpgradeType.DASH;

        invincibilityFramesCount = 0;
        dirDecisionFramesCount = 0;
        dashFramesCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!dashing) return;

        if(dashing && !dirSet
            && dirDecisionFramesCount++ < dirDecisionFrames)
        {
            Debug.Log("Assigning direction");
            foreach(Player.UserInput key in 
                GetComponent<Movement>().MovementDictionary.Keys)
            {
                if (GetComponent<Movement>().MovementDictionary[key] == null) continue;

                if(Input.GetKey((KeyCode)GetComponent<Movement>().MovementDictionary[key]))
                {
                    MoveDir(key);
                }
            }
        }
        else if(dashing && !dirSet 
            && dirDecisionFramesCount++ >= dirDecisionFrames)
        {
            Debug.Log("Direction not chosen, setting direction");
            MoveDir(Player.UserInput.MOVERIGHT);
        } else if (dashing && dirDecisionFramesCount > dirDecisionFrames)
        {
            Debug.Log("Decision time is up");
            dirSet = true;
            dirDecisionFrames = 0;
        }
        else if(dashing && dirSet && dashFramesCount++ < dashFrames)
        {
            Debug.Log("Dash");
            if (GetComponent<Movement>().HorizontalMove)
            {
                GetComponent<Rigidbody2D>().velocity = dashDir * dashSpeed
                    + new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            } else if(GetComponent<Movement>().VerticalMove)
            {
                GetComponent<Rigidbody2D>().velocity = dashDir * dashSpeed
                    + new Vector2(0,GetComponent<Rigidbody2D>().velocity.y);
            } else
            {
                GetComponent<Rigidbody2D>().velocity = dashDir * dashSpeed;
            }
        }
        else if(dashing && dirSet && dashFramesCount >= dashFrames)
        {
            Debug.Log("Dash over");
            dashDir = Vector2.zero;
            dirSet = false;
            dashing = false;
            dashFramesCount = 0;
            invincibilityFramesCount = 0;

            //Incase swimming is also attached
            foreach(Movement m in GetComponents<Movement>())
            {
                m.StartMovement();
            }
        }
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += SceneLoaded;
    }

    void SceneLoaded(Scene s, LoadSceneMode lsm)
    {
        dashFramesCount = 0;
        invincibilityFramesCount = 0;
        dirDecisionFramesCount = 0;
    }

    /// <summary>
    /// Set's the dash variable to true, letting the update to call the appropriate function
    /// </summary>
    public override void PerformSpecial()
    {
        Debug.Log("Dashing");
        dashing = true;
    }

    /// <summary>
    /// Receiving a movement send it through a switch statement to figure out the dash dir
    /// </summary>
    private void MoveDir(Player.UserInput dir)
    {
        switch (dir)
        {
            case Player.UserInput.MOVEUP:
                dashDir += Vector2.up;
                //Incase swimming is also attached
                foreach (Movement m in GetComponents<Movement>())
                {
                    m.StopMovement(false, true);
                }
                break;
            case Player.UserInput.MOVERIGHT:
                dashDir += Vector2.right;
                foreach (Movement m in GetComponents<Movement>())
                {
                    m.StopMovement(true, false);
                }
                break;
            case Player.UserInput.MOVEDOWN:
                dashDir += Vector2.down;
                foreach (Movement m in GetComponents<Movement>())
                {
                    m.StopMovement(false, true);
                }
                break;
            case Player.UserInput.MOVELEFT:
                dashDir += Vector2.left;
                foreach (Movement m in GetComponents<Movement>())
                {
                    m.StopMovement(true, false);
                }
                break;
            default:
                return;
        }

        Debug.Log("Dash Direction set");
        if(dirDecisionFramesCount < dirDecisionFrames - 3)
        {
            dirDecisionFramesCount = dirDecisionFrames - 3;
        } else
        {
            dirSet = true;
            dirDecisionFramesCount = 0;
            if (GetComponent<CombatSystem>() != null)
            {
                GetComponent<CombatSystem>().SetInvinicbility(invincibilityFrames);
            }
        }
    }
}
