using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbitter : MonoBehaviour
{
    public Transform CenterOfGravity { get; set; }

    private float currentRotation = 0;
    [SerializeField]
    private float rotationSpeed;
    public float RotationSpeed { get { return rotationSpeed; } }

    void Update()
    {
        currentRotation = currentRotation + rotationSpeed;
        if(currentRotation >= 360)
        {
            currentRotation = 0;
        }
        transform.Rotate(transform.forward, currentRotation);
    }

    void FixedUpdate()
    {
        Vector3 difference = CenterOfGravity.position - transform.position;

        float dist = difference.magnitude;
        Vector3 gravityDirection = difference.normalized;
        // F = G((m1*m2)/(r*r))
        float gravity = 6.7f * (CenterOfGravity.localScale.x * transform.localScale.x * 80) / (dist * dist);

        Vector3 gravityVector = gravityDirection * gravity;
        GetComponent<Rigidbody>().AddForce(transform.forward, ForceMode.Acceleration);
        GetComponent<Rigidbody>().AddForce(gravityVector, ForceMode.Acceleration);
    }
}
