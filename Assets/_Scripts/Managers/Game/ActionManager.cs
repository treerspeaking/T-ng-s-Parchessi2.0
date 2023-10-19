
using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Player.Pawn;
using Shun_Unity_Editor;
using UnityEngine;
using UnityUtilities;

public class ActionManager : SingletonMonoBehaviour<ActionManager>
{
    
    [SerializeField, ShowImmutable] private List<TargetEntity> _mapTargets;

    private PlayerActionController PlayerActionController =>
        GameManager.Instance.ClientOwnerPlayerController.PlayerActionController;
    
    public void AddTargetEntity(TargetEntity targetEntity)
    {
        _mapTargets.Add(targetEntity);
    }

    
    public void ExecuteTarget<TTargeter>(TTargeter targeterMonoBehaviour, TargetEntity targetEntity) 
        where TTargeter : PlayerEntity, ITargeter<TTargeter>
    
    {
        if (targeterMonoBehaviour == null || targetEntity == null)
        {
            Debug.Log("Targeter or targetee is null");
            return;
        }
        var targeteeMonoBehaviour = targetEntity.Get<PlayerEntity>();
        SendTargetExecutionToServer(targeterMonoBehaviour, targeteeMonoBehaviour);
        targeteeMonoBehaviour.ExecuteTargetee(targeterMonoBehaviour);
        targeterMonoBehaviour.ExecuteTargeter(targeteeMonoBehaviour);
    }
    
    public void ExecuteTarget<TTargeter, TTargetee>(TTargeter targeterMonoBehaviour, TTargetee targeteeMonoBehavior) 
        where TTargeter : PlayerEntity, ITargeter<TTargeter>
        where TTargetee : PlayerEntity, ITargetee<TTargetee>
    {
        if (targeterMonoBehaviour == null || targeteeMonoBehavior == null)
        {
            Debug.Log("Targeter or targetee is null");
            return;
        }
        
        SendTargetExecutionToServer(targeterMonoBehaviour, targeteeMonoBehavior.GetTarget());
        targeteeMonoBehavior.ExecuteTargetee(targeterMonoBehaviour);
        
    }

    public void SimulateTargetExecutionFromServer(TargetContainer targetContainer)
    {
        
    }

    private void SendTargetExecutionToServer<TTargeter, TTargetee>(TTargeter targeterMonoBehaviour, TTargetee targeteeMonoBehaviour)
        where TTargeter : PlayerEntity, ITargeter<TTargeter>
        where TTargetee : PlayerEntity, ITargetee<TTargetee>
    {
        var targetContainer = CreateTargetContainer(targeterMonoBehaviour, targeteeMonoBehaviour);

        PlayerActionController.PlayTargetServerRPC(targetContainer);
    }


    private TargetContainer CreateTargetContainer<TTargeter, TTargetee>(TTargeter targeterMonoBehaviour, TTargetee targeteeMonoBehaviour)
        where TTargeter : PlayerEntity, ITargeter<TTargeter>
        where TTargetee : PlayerEntity, ITargetee<TTargetee>
    {
        TargetContainer targetContainer = new TargetContainer();
        
        (targetContainer.TargeterType, targetContainer.TargeterContainerIndex) = GetTargetTypeAndContainerIndex(targeterMonoBehaviour);
        (targetContainer.TargeteeType, targetContainer.TargeteeContainerIndex) = GetTargetTypeAndContainerIndex(targeteeMonoBehaviour);
        
        return targetContainer;
        
    }

    private (TargetType, int) GetTargetTypeAndContainerIndex(PlayerEntity targeterMonoBehaviour)
    {
        if (targeterMonoBehaviour is HandDice) return (TargetType.Dice, targeterMonoBehaviour.ContainerIndex);
        else if (targeterMonoBehaviour is PlayerPawn)
            return (TargetType.Pawn, targeterMonoBehaviour.ContainerIndex);
        else return (TargetType.Deck, -1);
        
    }
    
    private T GetTargetEntity<T>(TargetType targetType, int containerIndex) where T : PlayerEntity
    {
        return targetType switch
        {
            TargetType.Dice => _mapTargets[containerIndex] as T,
            TargetType.Pawn => _mapTargets[containerIndex] as T,
            TargetType.Deck => _mapTargets[containerIndex] as T,
            _ => null
        };
    }
    
}
