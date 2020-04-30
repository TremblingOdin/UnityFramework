using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ellipsis : MonoBehaviour
{
    [SerializeField]
    private float xDistCenter;
    public float XDistCenter { get { return xDistCenter; } }
    [SerializeField]
    private float yDistCenter;
    public float YDistCenter { get { return yDistCenter; } }
    [SerializeField]
    private float zDistCenter;
    public float ZDistCenter { get { return zDistCenter; } }
    [SerializeField]
    private Vector3 center;
    public Vector3 Center { get { return center; } }
    [SerializeField]
    private Vector3 directionOfMajorAxis;
    public Vector3 DirectionOfMajorAxis { get { return directionOfMajorAxis; } }
    [SerializeField]
    private Vector3 directionOfMinorAxis;
    public Vector3 DirectionOfMinorAxis { get { return directionOfMinorAxis; } }
    [SerializeField]
    private Vector3 focusA;
    public Vector3 FocusA { get { return focusA; } }
    [SerializeField]
    private Vector3 focusB;
    public Vector3 FocusB { get { return focusB; } }
    [SerializeField]
    private Vector3 focusC;
    public Vector3 FocusC { get { return focusC; } }
    [SerializeField]
    private bool isSphere;
    public bool IsSphere { get { return isSphere; } }
    [SerializeField]
    private bool isCircle;
    public bool IsCircle { get { return isCircle; } }

    public float DistanceBetweenFoci { get; set; }

    private float perimeter;
    private float spaceBetweenNodes;

    public List<Node> Orbiters;
    public List<Vector3> travelPoints;

    public GameObject nodePrefab;

    void Awake()
    {
        //If this is supposed to be a Sphere
        //set focus B and C to A, and yDistCenter, to xDistCenter
        //if IsSphere and ThreeD zDistCenter will be set too
        if(IsSphere)
        {
            focusC = focusB = focusA;
            zDistCenter = yDistCenter = xDistCenter;
        } else if (IsCircle)
        {

        }

        DistanceBetweenFoci = (focusA - focusB).magnitude;
        perimeter = CalcCircumference();
        PopulateOrbit();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /// <summary>
    /// C = 2pi*sqrt((2a+2b)/2)
    /// Calculates the circumference of the Ellipse
    /// </summary>
    /// <returns>Circumference</returns>
    private float CalcCircumference()
    {
        float circ = Mathf.Sqrt((2 * xDistCenter + 2 * yDistCenter) / 2);
        circ *= 2 * Mathf.PI;
        return circ;
    }

    /// <summary>
    /// Fills the ellipse with nodes based on the spacing between them
    /// x = a cos t
    /// y = b cos t
    /// z = c cos t
    /// where 0 < t < 2pi
    /// </summary>
    private void PopulateOrbit()
    {
        int nodeCount = Mathf.FloorToInt(perimeter / (((RectTransform)nodePrefab.transform).rect.width + spaceBetweenNodes));
        float extraSpace = perimeter % nodeCount;
        spaceBetweenNodes += extraSpace / nodeCount;

        travelPoints = new List<Vector3>();
        for (float i = 0; i < 2*Mathf.PI; i+=(1/6)*Mathf.PI)
        {
            Vector3 location = new Vector3(xDistCenter * Mathf.Cos(i), yDistCenter * Mathf.Cos(i), zDistCenter * Mathf.Cos(i));
            travelPoints.Add(location);
            GameObject orbiter = Instantiate(nodePrefab) as GameObject;
            orbiter.transform.SetParent(transform);
            orbiter.transform.position = location;
            Orbiters.Add(orbiter.GetComponent<Node>());
        }
    }
}
