using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers.Network;
using QFSW.QC;
using Shun_Unity_Editor;
using Unity.Netcode;
using UnityEngine;
using UnityUtilities;


public class GameManager : SingletonNetworkBehavior<GameManager>
{

    public enum GameState
    {
        NetworkSetup,
        GameSetup,
        GamePlay,
        GamePause,
        GameEnd
    }

    [SerializeField, ShowImmutable] private List<PlayerController> _playerControllers = new();
    [SerializeField, ShowImmutable] private GameState _gameState = GameState.NetworkSetup;
    
    
    private readonly NetworkVariable<int> _playerTurnId = new NetworkVariable<int>(0);
    
    public PlayerController ClientOwnerPlayerController;
    public int PlayerTurn => _playerTurnId.Value;

    public Action OnNetworkSetUp { get; set; }
    public Action OnGameSetUp { get; set; }

    public void StartPlayerController(PlayerController playerController)
    {
        _playerControllers.Add(playerController);
        if (playerController.IsOwner)
        {
            ClientOwnerPlayerController = playerController;
            
            _gameState = GameState.GameSetup;
            OnGameSetUp.Invoke();
        }
    }


    [Command]
    public void StartGame()
    {
        _playerControllers[PlayerTurn].PlayerTurnController.StartPreparationPhaseServerRPC();
        _gameState = GameState.GamePlay;
    }
    
    
    [ServerRpc]
    public void StartNextPlayerTurnServerRPC()
    {
        _playerTurnId.Value++;
        if (PlayerTurn >= _playerControllers.Count)
        {
            _playerTurnId.Value = 0;
        }
        _playerControllers[PlayerTurn].PlayerTurnController.StartPreparationPhaseServerRPC();
        
        Debug.Log($"Player {_playerControllers[PlayerTurn].OwnerClientId} start turn");

    }
    
}