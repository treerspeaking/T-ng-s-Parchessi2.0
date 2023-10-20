using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Managers.Network;
using _Scripts.NetworkContainter;
using _Scripts.Scriptable_Objects;
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

    [SerializeField] public List<PlayerController> PlayerControllers { get; private set; } = new();
    [SerializeField, ShowImmutable] private GameState _gameState = GameState.NetworkSetup;
    
    
    private readonly NetworkVariable<int> _playerIdTurn = new NetworkVariable<int>(0);
    
    
    public PlayerController ClientOwnerPlayerController;
    public Action OnNetworkSetUp { get; set; }
    public Action OnPlayerJoinGameSetUp { get; set; }
    public Action OnGameStart { get; set; }
    public Action OnGameEnd { get; set; }
    public Action OnGamePause { get; set; }

    [SerializeField] private List<DiceDescription> _incomeDiceDescriptions = new();
    [SerializeField] private List<PawnDescription> _pawnDescriptions = new();
    [SerializeField] private List<CardDescription> _deckCardDescriptions = new();

    public PlayerController GetPlayerController(ulong clientId)
    {
        return PlayerControllers.FirstOrDefault(playerController => playerController.OwnerClientId == clientId);
    }

    public void StartPlayerController(PlayerController playerController)
    {
        PlayerControllers.Add(playerController);
        if (playerController.IsOwner)
        {
            ClientOwnerPlayerController = playerController;
            
            _gameState = GameState.GameSetup;
            OnPlayerJoinGameSetUp.Invoke();
            //OnPlayerJoinGameSetUp = null;
        }
    }


    [ServerRpc]
    [Command]
    public void StartGameServerRPC()
    {
        _gameState = GameState.GamePlay;
        StartGameClientRPC();
        LoadPlayerSetup();
        StartPlayerTurn(PlayerControllers[_playerIdTurn.Value]);
    }

    [ClientRpc]
    public void StartGameClientRPC()
    {
        
        OnGameStart.Invoke();
    }

    private void LoadPlayerSetup()
    {
        
        foreach (var playerController in PlayerControllers)
        {
            foreach (var diceDescription in _incomeDiceDescriptions)
            {
                playerController.PlayerResourceController.AddIncomeServerRPC(new DiceContainer
                {
                    DiceID = diceDescription.DiceID 
                });
                
                Debug.Log($"Dice {diceDescription.DiceID} : ");
                
            }

            foreach (var cardDescription in _deckCardDescriptions)
            {
                playerController.PlayerResourceController.AddCardToDeckServerRPC(new CardContainer
                {
                    CardID = cardDescription.CardID
                });
                
                Debug.Log($"Card {cardDescription.CardID} : ");
            }
        }

    }
    
    
    [ServerRpc]
    public void StartNextPlayerTurnServerRPC()
    {
        _playerIdTurn.Value++;
        if (_playerIdTurn.Value >= PlayerControllers.Count)
        {
            _playerIdTurn.Value = 0;
        }

        StartPlayerTurn(PlayerControllers[_playerIdTurn.Value]);
        
        Debug.Log($"Player {PlayerControllers[_playerIdTurn.Value].OwnerClientId} start turn");

    }

    private void StartPlayerTurn(PlayerController playerController)
    {
        playerController.PlayerTurnControllerRequire.StartPreparationPhaseServerRPC();
        playerController.PlayerResourceController.GainIncomeServerRPC();
    }
    
    
}