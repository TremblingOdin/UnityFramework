using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Buildable : MonoBehaviour
{
    [SerializeField]
    private bool rotateable;
    [SerializeField]
    private int nodeCount;

    public bool Rotateable { get { return rotateable; } }
    public int NodeCount { get { return NodeCount; } }

    public string Name { get; set; }
    public List<Buildable> Neighbors { get; set; }
    
    // Start is called before the first frame update
    void Awake()
    {
        Name = gameObject.name;
    }
}
