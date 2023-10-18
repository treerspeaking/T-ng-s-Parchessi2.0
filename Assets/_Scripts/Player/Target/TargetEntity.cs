using System;
using _Scripts.NetworkContainter;
using _Scripts.Player.Pawn;
using _Scripts.Player.Target;
using UnityEngine;


public class TargetEntity : MonoBehaviour
{
    private PlayerActionController PlayerActionController => GameManager.Instance.ClientOwnerPlayerController.PlayerActionController;
    private void Awake()
    {
        MapManager.Instance.AddTargetEntity(this);   
        
    }

    private TargetContainer CreateTargetContainer<TTargeter, TTargetee>(TTargeter targeterMonoBehaviour, TTargetee targeteeMonoBehaviour)
        where TTargeter : PlayerEntity, ITargeter<TTargeter>
        where TTargetee : PlayerEntity, ITargetee<TTargetee>
    {
        TargetContainer targetContainer = new TargetContainer();
        
        (targetContainer.TargeterType, targetContainer.TargeterContainerIndex) = GetTargetType(targeterMonoBehaviour);
        (targetContainer.TargeteeType, targetContainer.TargeteeContainerIndex) = GetTargetType(targeteeMonoBehaviour);
        
        return targetContainer;
        
    }

    private (TargetType, int) GetTargetType(PlayerEntity targeterMonoBehaviour)
    {
        if (targeterMonoBehaviour is HandDice) return (TargetType.Dice, targeterMonoBehaviour.ContainerIndex);
        else if (targeterMonoBehaviour is TestPawn)
            return (TargetType.Pawn, targeterMonoBehaviour.ContainerIndex);
        else return (TargetType.All, -1);
        
    }

    protected void SendExecuteToServer<TTargeter, TTargetee>(TTargeter targeterMonoBehaviour, TTargetee targeteeMonoBehaviour)
        where TTargeter : PlayerEntity, ITargeter<TTargeter>
        where TTargetee : PlayerEntity, ITargetee<TTargetee>
    {
        var targetContainer = CreateTargetContainer(targeterMonoBehaviour, targeteeMonoBehaviour);

        PlayerActionController.PlayTargetServerRPC(targetContainer);
    }
}
