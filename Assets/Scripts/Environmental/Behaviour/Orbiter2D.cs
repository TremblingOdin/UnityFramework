using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Orbiter2D : MonoBehaviour
{
    [SerializeField]
    private GameObject ellipsePrefab;

    [SerializeField]
    private float gravityConstant;
    public float GravityConstant { get { return gravityConstant; } }

    [SerializeField]
    private Transform center;
    public Transform Center { get { return center; } }

    [SerializeField]
    private float speed;
    public float Speed { get { return speed; } }

    [SerializeField]
    private float rotationSpeed;

    private float currentRotation;
    private float apsisDistance; //periapsis is the closest point in an orbit
    private float startingAngle;

    public bool CircularOrbit { get; set; }
    public bool CounterClockwise { get; set; }
    public float DistanceToCenter { get; private set; }
    public bool Orbiting { get; set; }

    public void Awake()
    {
        GetComponent<Rigidbody2D>().drag = 0;
        GetComponent<Rigidbody2D>().gravityScale = 0;
        GetComponent<Rigidbody2D>().isKinematic = false;

        currentRotation = 0f;

        if (center != null)
        {
            Vector3 distance3D = transform.position - center.position;
            DistanceToCenter = distance3D.magnitude;
            Orbiting = true;
        }
    }

    public void FixedUpdate()
    {
        RotateAndOrbit();
    }

    /// <summary>
    /// Reassign the transform center and recalculate the distance to center
    /// </summary>
    /// <param name="center">New parent to orbit</param>
    public void ChangeOrbit(Transform center)
    {
        this.center = center;
        Vector3 distance3D = transform.position - center.position;
        DistanceToCenter = distance3D.magnitude;
    }

    /// <summary>
    /// Rotate this object around it's center, then rotate it around the target center
    /// </summary>
    public void RotateAndOrbit()
    {
        //Rotate self
        currentRotation = currentRotation + (1 * rotationSpeed);
        if (currentRotation >= 360)
            currentRotation = 0f;

        transform.Rotate(Vector3.forward, currentRotation);

        while(Orbiting)
        {
            Vector3 difference = transform.position - center.position;
            GetComponent<Rigidbody2D>().AddForce(
                -difference.normalized * Speed * gravityConstant * Time.fixedDeltaTime / difference.sqrMagnitude);
        }
    }
}
