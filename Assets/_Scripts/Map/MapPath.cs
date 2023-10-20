using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Map
{
    [Serializable]
    public class MapPath
    {
        public List<MapCell> Path = new ();
    }
}