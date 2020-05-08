using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Liquid2D : MonoBehaviour
{
    [SerializeField]
    private Vector3 bouyantForce;
    public Vector3 BouyantForce { get { return bouyantForce; } set { bouyantForce = value; } }

    [SerializeField]
    private float gravityScale;
    public float GravityScale { get { return gravityScale; } }

    [SerializeField]
    private float angleDrag;
    public float AngleDrag { get { return angleDrag; } }

    [SerializeField]
    private int damage;
    public int Damage { get { return damage; } }

    [SerializeField]
    private bool instaDeath;
    public bool InstaDeath { get { return instaDeath; } }
}
