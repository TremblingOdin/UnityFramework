using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallCheck2D : Special
{
    public bool RightWall { get; set; }

    [SerializeField]
    private BasePlatformer2D player;

    void OnEnable()
    {
        player = GetComponent<BasePlatformer2D>();
    }

    void Update()
    {
        if(player.TouchingWall)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            player.CanJump = true;

            if(!player.IsGrounded)
            {
                if (RightWall)
                {
                    player.FaceRight = false;
                }
                else
                {
                    player.FaceRight = true;
                }
            }
        }
    }
}
