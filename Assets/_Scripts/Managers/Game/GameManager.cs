using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Managers.Network;
using _Scripts.NetworkContainter;
using QFSW.QC;
using Shun_Unity_Editor;
using Unity.Netcode;
using UnityEditor;
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
    
    
    private readonly NetworkVariable<int> _playerIdTurn = new NetworkVariable<int>(0);
    
    
    public PlayerController ClientOwnerPlayerController;
    public Action OnNetworkSetUp { get; set; }
    public Action OnGameSetUp { get; set; }

    [SerializeField] private List<DiceDescription> _incomeDiceContainers = new();

    public PlayerController GetPlayerController(ulong clientId)
    {
        return _playerControllers.FirstOrDefault(playerController => playerController.OwnerClientId == clientId);
    }

    public void StartPlayerController(PlayerController playerController)
    {
        _playerControllers.Add(playerController);
        if (playerController.IsOwner)
        {
            ClientOwnerPlayerController = playerController;
            
            _gameState = GameState.GameSetup;
            OnGameSetUp.Invoke();
            //OnGameSetUp = null;
        }
    }


    [Command]
    public void StartGame()
    {
        _gameState = GameState.GamePlay;
        
        foreach (var playerController in _playerControllers)
        {
            foreach (var diceDescription in _incomeDiceContainers)
            {
                playerController.PlayerResourceController.AddIncomeServerRPC(new DiceContainer
                {
                    DiceID = diceDescription.DiceID 
                });
                
                Debug.Log($"Dice {diceDescription.DiceID} : ");
                
            }
        }
        
        StartPlayerTurn(_playerControllers[_playerIdTurn.Value]);
    }
    
    
    [ServerRpc]
    public void StartNextPlayerTurnServerRPC()
    {
        _playerIdTurn.Value++;
        if (_playerIdTurn.Value >= _playerControllers.Count)
        {
            _playerIdTurn.Value = 0;
        }

        StartPlayerTurn(_playerControllers[_playerIdTurn.Value]);
        
        Debug.Log($"Player {_playerControllers[_playerIdTurn.Value].OwnerClientId} start turn");

    }

    private void StartPlayerTurn(PlayerController playerController)
    {
        playerController.PlayerTurnController.StartPreparationPhaseServerRPC();
        playerController.PlayerResourceController.GainIncomeServerRPC();
    }
    
    
}