
using System.Collections.Generic;
using UnityEngine;
using UnityUtilities;

public class MapManager : SingletonMonoBehaviour<MapManager>
{
    [SerializeField] private List<TargetEntity> _mapTargets; 
    
    public void AddTargetEntity(TargetEntity targetEntity)
    {
        
    }
    
}
