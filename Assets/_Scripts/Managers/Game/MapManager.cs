using System;
using System.Collections.Generic;
using _Scripts.Managers.Network;
using _Scripts.Map;
using UnityEngine;

namespace _Scripts.Managers.Game
{
    public class MapManager : SingletonNetworkBehavior<MapManager>
    {
        [SerializeField] private List<MapPath> _mapPaths = new ();

    }
}