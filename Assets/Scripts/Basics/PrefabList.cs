using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(menuName = "Prefab List")]
public class PrefabList : ScriptableObject
{
    //Add to this whatever prefabs you need for a system type
    public Orbiter2D orbiterPrefab;
    public OrbitShape2D ellipsesPrefab;
}
