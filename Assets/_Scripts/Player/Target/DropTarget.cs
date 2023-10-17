using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Player.Pawn;
using _Scripts.Player.Target;
using UnityEngine;


public abstract class DropTarget<T> : PlayerControllerStartSetUpDependency where T : PlayerEntity
{
    private ITargetee<T> _targetBehaviour;
    
    [SerializeField] private TargetType _targetType;

    protected override void Awake()
    {
        base.Awake();
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
        
        var targetContainer = CreateTargetContainer(targeterMonoBehaviour);
        PlayerController.PlayerActionController.PlayTargetServerRPC(targetContainer);
        _targetBehaviour.ExecuteTarget(targeterMonoBehaviour);
        
    }
    
    private TargetContainer CreateTargetContainer<TTargeter>(TTargeter targeterMonoBehaviour) where TTargeter : PlayerEntity, ITargeter<TTargeter>
    {
        TargetContainer targetContainer = new TargetContainer();
        
        (targetContainer.TargeterType, targetContainer.TargeterContainerIndex) = GetTargetType(targeterMonoBehaviour);
        (targetContainer.TargeterType, targetContainer.TargeterContainerIndex) = GetTargetType(_targetBehaviour.GetTarget());
        
        return targetContainer;
        
    }
    
    private (TargetType, int) GetTargetType(PlayerEntity targeterMonoBehaviour)
    {
        if (targeterMonoBehaviour is HandDice) return (TargetType.Dice, targeterMonoBehaviour.ContainerIndex);
        else if (targeterMonoBehaviour is TestPawn)
            return (TargetType.Pawn, targeterMonoBehaviour.ContainerIndex);
        else return (TargetType.All, -1);
        
    }
}
