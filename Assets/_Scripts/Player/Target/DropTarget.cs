using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public abstract class DropTarget : MonoBehaviour
{
    [SerializeField] private MonoBehaviour _targetBehaviour;
    [SerializeField] private TargetType _targetType;
    private T GetTarget<T>() where T : MonoBehaviour
    {
        return (T) _targetBehaviour;
    }
    
    public abstract void ExecuteDrop(DragAndDrop dragAndDrop);
}
