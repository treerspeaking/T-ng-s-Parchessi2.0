using System;
using System.Collections;
using System.Collections.Generic;

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
    
    [ShowImmutable, SerializeField] private NetworkVariable<PlayerPhase> _currentPlayerPhase = new NetworkVariable<PlayerPhase>(PlayerPhase.Wait);

    private void Start()
    {
        Debug.Log("Start Player Turn Controller");
    }

    [Command]
    public PlayerPhase GetPlayerPhase()
    {
        return _currentPlayerPhase.Value;
    }
    
    [ClientRpc]
    private void StartPreparationPhaseClientRPC()
    {
        Debug.Log($"Client {OwnerClientId} Start Preparation");
    }

    [ServerRpc(RequireOwnership = false), Command]
    public void StartPreparationPhaseServerRPC()
    {
        _currentPlayerPhase.Value = PlayerPhase.Preparation;
        
        StartPreparationPhaseClientRPC();
    }
    
    [ClientRpc]
    private void StartRollPhaseClientRPC()
    {
        Debug.Log($"Client {OwnerClientId} Start Preparation");
    }
    
    [ServerRpc, Command]
    private void StartRollPhaseServerRPC()
    {
        _currentPlayerPhase.Value = PlayerPhase.Roll;
        StartRollPhaseClientRPC();
    }
   
    [ClientRpc]
    private void StartSubsequencePhaseClientRPC()
    {
    }
    
    [ServerRpc, Command]
    private void StartSubsequencePhaseServerRPC()
    {
        _currentPlayerPhase.Value = PlayerPhase.Subsequence;
        StartSubsequencePhaseClientRPC();
    }
    
    [ClientRpc]
    private void EndTurnClientRPC()
    {
        Debug.Log($"Client {OwnerClientId} End Turn");
        GameManager.Instance.StartNextPlayerTurn();
    }
    
    [ServerRpc(RequireOwnership = false), Command]
    public void EndTurnServerRPC()
    {
        _currentPlayerPhase.Value = PlayerPhase.Wait;
        EndTurnClientRPC();
    }
    
}
