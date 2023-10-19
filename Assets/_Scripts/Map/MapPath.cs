using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Map
{
    [Serializable]
    public class MapPath
    {
        [SerializeField] private List<MapCell> _path = new ();
    }
}