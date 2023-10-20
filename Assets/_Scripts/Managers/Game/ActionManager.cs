
using System;
using System.Collections.Generic;
using _Scripts.Managers.Game;
using _Scripts.NetworkContainter;
using _Scripts.Player.Dice;
using _Scripts.Player.Pawn;
using _Scripts.Simulation;
using Shun_Unity_Editor;
using UnityEngine;
using UnityUtilities;

public class ActionManager : SingletonMonoBehaviour<ActionManager>
{
    
    [SerializeField, ShowImmutable] private List<TargetEntity> _mapTargets;

    public void AddTargetEntity(TargetEntity targetEntity)
    {
        _mapTargets.Add(targetEntity);
    }
    
    
    public void ExecuteTarget<TTargeter, TTargetee>(TTargeter targeter, TTargetee targetee) 
        where TTargeter : ITargeter
        where TTargetee : ITargetee
    {
        if (targeter == null || targetee == null)
        {
            Debug.Log("Targeter or targetee is null");
            return;
        }
        
        SendTargetExecutionToServer(targeter, targetee);
        
    }

    public async void SimulateAction(ActionContainer actionContainer)
    {
        var targeter = GetTargeter(actionContainer.TargeterContainer);
        var targetee = GetTargetee(actionContainer.TargeteeContainer);

        CoroutineSimulationManager.Instance.AddCoroutineSimulationObject(targeter.ExecuteTargeter(targetee));
        CoroutineSimulationManager.Instance.AddCoroutineSimulationObject(targetee.ExecuteTargetee(targeter));
        CoroutineSimulationManager.Instance.ExecuteAllCoroutineSimulationsThenClear();
    }


    private ITargeter GetTargeter(TargetContainer targeterContainer)
    {
        switch (targeterContainer.TargetType)
        {
            case TargetType.Empty:
                break;
            case TargetType.Pawn:
                break;
            case TargetType.Tile:
                break;
            case TargetType.Card:
                return GameManager.Instance.GetPlayerController(targeterContainer.TargetClientOwnerId)
                    .PlayerResourceController.PlayerCardHand.GetHandCard(targeterContainer.TargetContainerIndex);
                break;
            case TargetType.Dice:
                return GameManager.Instance.GetPlayerController(targeterContainer.TargetClientOwnerId)
                    .PlayerResourceController.PlayerDiceHand.GetHandDice(targeterContainer.TargetContainerIndex);
            case TargetType.DiceConverter:
            default:
                return null;
        }

        return null;
    }
    
    private ITargetee GetTargetee(TargetContainer targeteeContainer)
    {
        switch (targeteeContainer.TargetType)
        {
            case TargetType.Empty:
                break;
            case TargetType.DiceConverter:
                return MapManager.Instance.GetDiceCardConverter();
                break;
            case TargetType.Pawn:
                return MapManager.Instance.GetPlayerPawn(targeteeContainer.TargetContainerIndex);
                break;
            case TargetType.Tile:
                break;
            case TargetType.Card:
                return GameManager.Instance.GetPlayerController(targeteeContainer.TargetClientOwnerId)
                    .PlayerResourceController.PlayerCardHand.GetHandCard(targeteeContainer.TargetContainerIndex);
                break;
            case TargetType.Dice:
                return GameManager.Instance.GetPlayerController(targeteeContainer.TargetClientOwnerId)
                    .PlayerResourceController.PlayerDiceHand.GetHandDice(targeteeContainer.TargetContainerIndex);
                break;
            default:
                return null;
        }

        return null;
    }


    private void SendTargetExecutionToServer<TTargeter, TTargetee>(TTargeter targeterMonoBehaviour, TTargetee targeteeMonoBehaviour)
        where TTargeter : ITargeter
        where TTargetee : ITargetee
    {
        var targetContainer = CreateActionContainer(targeterMonoBehaviour, targeteeMonoBehaviour);

        GameManager.Instance.ClientOwnerPlayerController.PlayerActionController.PlayTargetServerRPC(targetContainer);
    }


    private ActionContainer CreateActionContainer<TTargeter, TTargetee>(TTargeter targeter, TTargetee targetee)
        where TTargeter : ITargeter
        where TTargetee : ITargetee
    {
        
        TargetContainer targeterContainer = GetTargetContainer(targeter);
        TargetContainer targeteeContainer = GetTargetContainer(targetee);
        
        return new ActionContainer
        {
            TargeterContainer = targeterContainer,
            TargeteeContainer = targeteeContainer
        };
        
    }

    private TargetContainer GetTargetContainer(ITargeter targeter)
    {
        var monoBehavior = targeter.GetMonoBehavior();

        TargetType targetType = monoBehavior switch
        {
            HandDice => TargetType.Dice,
            MapPawn => TargetType.Pawn,
            DiceCardConverter => TargetType.DiceConverter,
            HandCard => TargetType.Card,
            _ => TargetType.Empty
        };

        return new TargetContainer
        {
            TargetType = targetType,
            TargetClientOwnerId = targeter.ClientOwnerID,
            TargetContainerIndex = targeter.ContainerIndex
        };
        
    }
    
    
    private TargetContainer GetTargetContainer(ITargetee targeter)
    {
        var monoBehavior = targeter.GetMonoBehavior();
        
        TargetType targetType;
        if (monoBehavior is HandDice) targetType = TargetType.Dice;
        else if (monoBehavior is MapPawn) targetType = TargetType.Pawn;
        else if (monoBehavior is DiceCardConverter) targetType = TargetType.DiceConverter;
        else targetType = TargetType.Empty; 

        return new TargetContainer
        {
            TargetType = targetType,
            TargetClientOwnerId = targeter.ClientOwnerID,
            TargetContainerIndex = targeter.ContainerIndex
        };
        
    }
    
    
    
}
