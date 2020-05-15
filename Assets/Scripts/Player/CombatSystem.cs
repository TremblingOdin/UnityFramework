using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatSystem : MonoBehaviour
{
    private bool invincible;
    private int inviFramecount;

    [SerializeField]
    private BoxCollider2D hurtBox;

    protected List<KeyCode> attacks;

    protected virtual void Awake()
    {
        attacks = new List<KeyCode>();
    }

    // Update is called once per frame
    void Update()
    {
        if(invincible && inviFramecount-- > 0)
        {
            hurtBox.enabled = false;
        } else
        {
            invincible = false;
            hurtBox.enabled = true;
        }
    }

    public void SetInvinicbility(int frames)
    {
        invincible = true;
        inviFramecount = frames;
    }

    public virtual void LoadAttack(Dictionary<KeyCode, Player.UserInput> attackSettings)
    {
        return;
    }
}
