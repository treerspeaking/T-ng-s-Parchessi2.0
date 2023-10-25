using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using _Scripts.Map;

public class MapRegion : MonoBehaviour
{
    [SerializeField] private Transform _mapParent;
    [SerializeField] private List<MapPath> _mapPaths = new ();
    // Start is called before the first frame update
    
    public MapPath GetMapPath(int pathIndex)
    {
        if (pathIndex < _mapPaths.Count)
        {
            return _mapPaths[pathIndex];
        }
        else
        {
            Debug.LogError("MapPath index out of range");
        }
        return null;
    }
}
