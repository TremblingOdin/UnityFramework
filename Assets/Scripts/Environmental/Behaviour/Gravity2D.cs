using System.Collections.Generic;
using UnityEngine;

public class Gravity2D : MonoBehaviour
{
    [SerializeField]
    private float range = 1000;
    public float Range { get { return range; } }

    Rigidbody2D ownRb;

    [SerializeField]
    ContactFilter2D cfd;

    public void Start()
    {
        ownRb = GetComponent<Rigidbody2D>();
    }

    public void FixedUpdate()
    {
        List<Collider2D> cols = new List<Collider2D>();
        Physics2D.OverlapCircle(transform.position, range, cfd, cols);
        List<Rigidbody2D> rbs = new List<Rigidbody2D>();

        foreach(Collider2D c in cols)
        {
            Rigidbody2D rb = c.attachedRigidbody;
            if(rb != null && rb != ownRb && !rbs.Contains(rb)) {
                rbs.Add(rb);
                Vector3 offset = transform.position - c.transform.position;
                rb.AddForce(offset / offset.sqrMagnitude * ownRb.mass);
            }
        }
    }

    public void OnGrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
