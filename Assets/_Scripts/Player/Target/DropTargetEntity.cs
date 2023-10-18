using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Player.Pawn;
using _Scripts.Player.Target;
using UnityEngine;


public abstract class DropTargetEntity<T> : TargetEntity where T : PlayerEntity, ITargetee<T>
{
    private ITargetee<T> _targetBehaviour;
    
    [SerializeField] private TargetType _targetType;

    protected void Awake()
    {
        _targetBehaviour = GetComponent<ITargetee<T>>();
    }

    public TargetType GetTargetType()
    {
        return _targetType;
    }
    
    public void ExecuteDrop<TTargeter>(TTargeter targeterMonoBehaviour) where TTargeter : PlayerEntity, ITargeter<TTargeter>
    {
        if (targeterMonoBehaviour == null || _targetBehaviour == null)
        {
            Debug.Log("Targeter or targetee is null");
            return;
        }
        
        SendExecuteToServer(targeterMonoBehaviour, _targetBehaviour.GetTarget());
        _targetBehaviour.ExecuteTarget(targeterMonoBehaviour);
        
    }
    
}
