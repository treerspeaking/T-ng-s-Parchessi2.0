using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Player;
using QFSW.QC;
using Shun_Unity_Editor;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerTurnController : PlayerControllerDependency
{
    public enum PlayerPhase : byte
    {
        Wait,
        Preparation,
        Roll,
        Subsequence
    }

    private PlayerController _playerController;
    [ShowImmutable] public NativeList<DiceContainer> CurrentTurnDices;
    [ShowImmutable] public readonly NetworkVariable<PlayerPhase> CurrentPlayerPhase = new NetworkVariable<PlayerPhase>(PlayerPhase.Wait);

    private PlayerDiceHand _playerDiceHand;
    private void Start()
    {
        CurrentTurnDices = new(Allocator.Persistent);
        _playerDiceHand = FindObjectOfType<PlayerDiceHand>();
    }

    [Command]
    public PlayerPhase GetPlayerPhase()
    {
        return CurrentPlayerPhase.Value;
    }
    
    [ClientRpc]
    private void StartPreparationPhaseClientRPC()
    {
        Debug.Log($"Client {OwnerClientId} Start Preparation");
    }

    [ServerRpc(RequireOwnership = false), Command]
    public void StartPreparationPhaseServerRPC()
    {
        CurrentPlayerPhase.Value = PlayerPhase.Preparation;
        
        StartPreparationPhaseClientRPC();
    }
    
    [ClientRpc]
    private void StartRollPhaseClientRPC()
    {
        Debug.Log($"Client {OwnerClientId} Start Roll");
        
    }
    
    [ServerRpc, Command]
    public void StartRollPhaseServerRPC()
    {
        CurrentPlayerPhase.Value = PlayerPhase.Roll;
        StartRollPhaseClientRPC();
    }
   
    [ClientRpc]
    private void StartSubsequencePhaseClientRPC()
    {
        Debug.Log($"Client {OwnerClientId} Start Subsequence");   
    }

    [ServerRpc]
    public void EndRollPhaseServerRPC()
    {
        if (CurrentTurnDices.Length > 0)
        {
            StartPreparationPhaseServerRPC();
        }
        else
        {
            StartSubsequencePhaseServerRPC();
        }
    }
    
    [ServerRpc]
    private void StartSubsequencePhaseServerRPC()
    {
        CurrentPlayerPhase.Value = PlayerPhase.Subsequence;
        StartSubsequencePhaseClientRPC();
    }
    
    [ClientRpc]
    private void EndTurnClientRPC()
    {
        Debug.Log($"Client {OwnerClientId} End Turn");
    }
    
    [ServerRpc(RequireOwnership = false), Command]
    public void EndTurnServerRPC()
    {
        CurrentPlayerPhase.Value = PlayerPhase.Wait;
        EndTurnClientRPC();
        
        GameManager.Instance.StartNextPlayerTurnServerRPC();
    }


    [ServerRpc]
    public void AddDiceServerRPC(DiceContainer diceContainer)
    {
        CurrentTurnDices.Add(diceContainer);
    }

    [ServerRpc]
    public void RemoveBackDiceServerRPC()
    {
        CurrentTurnDices.RemoveAt(CurrentTurnDices.Length - 1);    
    }

    
    
}
