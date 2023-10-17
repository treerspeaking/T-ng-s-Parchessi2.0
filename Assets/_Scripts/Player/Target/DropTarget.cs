using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Player.Target;
using UnityEngine;


public class DropTarget : MonoBehaviour
{
    [SerializeField] private ITargetable _targetBehaviour;
    
    [SerializeField] private TargetType _targetType;

    public void SetTargetedObject(ITargetable targetable)
    {
        _targetBehaviour = targetable;
    }

    public TargetType GetTargetType()
    {
        return _targetType;
    }
    
    public void ExecuteDrop<T>(T dragAndDropSelection) where T : MonoBehaviour
    {
        _targetBehaviour?.ExecuteTarget(dragAndDropSelection);
    }
}
