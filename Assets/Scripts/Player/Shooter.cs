using System.Collections.Generic;
using UnityEngine;

public class Shooter : CombatSystem
{
    [SerializeField]
    private Transform firepoint;

    [SerializeField]
    private GameObject[] bulletPrefabs;

    private GameObject bulletSelection;

    protected override void Awake()
    {
        base.Awake();
        bulletSelection = bulletPrefabs[0];
    }

    void Update()
    {
        if(GetComponent<Movement>().FaceRight && firepoint.localPosition.x < 0)
        {
            firepoint.localPosition = new Vector2(-firepoint.localPosition.x, firepoint.localPosition.y);
        }
        else if(!GetComponent<Movement>().FaceRight && firepoint.localPosition.x > 0)
        {
            firepoint.localPosition = new Vector2(-firepoint.localPosition.x, firepoint.localPosition.y);
        }

        foreach (KeyCode key in attacks) {
            if (Input.GetKeyDown(key))
            {
                //Instantiate(bulletSelection);
                Debug.Log("Shoot");
            }
        }
    }


    public override void LoadAttack(Dictionary<KeyCode, Player.UserInput> attackSettings)
    {
        foreach(KeyCode key in attackSettings.Keys)
        {
            Debug.Log(key);
            if(attackSettings[key] == Player.UserInput.ATTACK)
            {
                attacks.Add(key);
            }
        }
    }
}
