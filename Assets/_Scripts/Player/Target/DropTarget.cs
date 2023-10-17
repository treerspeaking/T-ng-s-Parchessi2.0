using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Player.Target;
using UnityEngine;


public abstract class DropTarget<T> : MonoBehaviour where T : MonoBehaviour
{
    private ITargetee<T> _targetBehaviour;
    
    [SerializeField] private TargetType _targetType;

    private void Awake()
    {
        _targetBehaviour = GetComponent<ITargetee<T>>();
    }

    /*
    public void Initialize(ITargetee<T> targetee)
    {
        _targetBehaviour = targetee;
    }
    */
    
    public TargetType GetTargetType()
    {
        return _targetType;
    }
    
    public void ExecuteDrop<TTargeter>(TTargeter targeterMonoBehaviour) where TTargeter : MonoBehaviour, ITargeter<TTargeter>
    {
        _targetBehaviour?.ExecuteTarget(targeterMonoBehaviour);
    }
}
