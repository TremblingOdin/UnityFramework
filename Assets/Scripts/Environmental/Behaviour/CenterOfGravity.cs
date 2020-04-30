using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CenterOfGravity : MonoBehaviour
{
    [SerializeField]
    private int sphereCount;
    public int SphereCount { get { return sphereCount; } }

    [SerializeField]
    private int maxRadius;
    public int MaxRadius { get { return maxRadius; } }

    [SerializeField]
    private Orbitter[] orbits;
    public Orbitter[] Orbits { get { return orbits; } }

    // The orbitTemplate needs it's gravity set to 0
    [Tooltip("Gravity should be set to false, or 0")]
    [SerializeField]
    private Orbitter orbitTemplate;

    void Awake()
    {
        orbits = new Orbitter[sphereCount];
    }

    void Start()
    {
        orbits = CreateOrbits(sphereCount, maxRadius);
    }

    /// <summary>
    /// Creates Orbital objects within the maximum radius away from the orbitter
    /// </summary>
    /// <param name="count">Number of objects to create</param>
    /// <param name="radius">Distance to create them at</param>
    /// <returns>Array of Objects Created</returns>
    public Orbitter[] CreateOrbits(int count, int radius)
    {
        var obs = new Orbitter[count];
        
        for (int i = 0; i < count; i++)
        {
            var ob = Instantiate(orbitTemplate) as Orbitter;
            ob.transform.position = transform.position +
                                    new Vector3(Random.Range(-maxRadius, maxRadius),
                                                Random.Range(-10, 10),
                                                Random.Range(-maxRadius, maxRadius));

            ob.transform.localScale *= Random.Range(0.5f, 1);
            ob.CenterOfGravity = transform;
            ob.GetComponent<Rigidbody>().AddForce(transform.right * 5, ForceMode.Impulse);
            orbits[i] = ob;
        }

        return orbits;
    }
}