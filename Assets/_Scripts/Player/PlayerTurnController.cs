using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Player;
using QFSW.QC;
using Shun_Unity_Editor;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerTurnController : PlayerControllerDependency
{
    public enum PlayerPhase
    {
        Wait,
        Preparation,
        Roll,
        Subsequence
    }

    private PlayerController _playerController;
    
    [ShowImmutable] private PlayerPhase _currentPlayerPhase;
    public PlayerPhase CurrentPlayerPhase => _currentPlayerPhase;

    private void Start()
    {
        Debug.Log("Start Player Turn Controller");
    }
    

    [Command]
    public PlayerPhase GetPlayerPhase()
    {
        return CurrentPlayerPhase;
    }
    
    [ClientRpc]
    private void StartPreparationPhaseClientRPC()
    {
        _currentPlayerPhase = PlayerPhase.Preparation;
        Debug.Log("Player Start Preparation");
        
    }

    [ServerRpc, Command]
    private void StartPreparationPhaseServerRPC()
    {
        StartPreparationPhaseClientRPC();
    }
    
    [ClientRpc]
    private void StartRollPhaseClientRPC()
    {
        _currentPlayerPhase = PlayerPhase.Roll;
    }
    
    [ServerRpc, Command]
    private void StartRollPhaseServerRPC()
    {
        StartRollPhaseClientRPC();
    }
   
    [ClientRpc]
    private void StartSubsequencePhaseClientRPC()
    {
        _currentPlayerPhase = PlayerPhase.Subsequence;
    }
    
    [ServerRpc, Command]
    private void StartSubsequencePhaseServerRPC()
    {
        StartSubsequencePhaseClientRPC();
    }
    
    [ClientRpc]
    private void EndTurnClientRPC()
    {
        _currentPlayerPhase = PlayerPhase.Wait;
    }
    
    [ServerRpc]
    private void EndTurnServerRPC()
    {
        EndTurnClientRPC();
    }
    
}
