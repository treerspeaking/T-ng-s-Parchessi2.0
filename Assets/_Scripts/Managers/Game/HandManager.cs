using System;
using System.Collections.Generic;
using _Scripts.Player;
using _Scripts.Player.Dice;
using Shun_Unity_Editor;
using UnityEngine;
using UnityUtilities;

namespace _Scripts.Managers.Game
{
    public class HandManager : SingletonMonoBehaviour<HandManager>
    {
        

        [SerializeField] private Transform _playerCardHandParent;
        [SerializeField] private Transform _playerDiceHandParent;
        [SerializeField] private Transform _offScreenCardHandParent;
        [SerializeField] private Transform _offScreenDiceHandParent;
        
        
        private HandDraggableObjectMouseInput _draggableObjectMouseInput = new ();
        private bool _isHandInteractable = false;
        
        private Dictionary<ulong,PlayerCardHand> _playerCardHands = new ();
        private Dictionary<ulong,PlayerDiceHand> _playerDiceHands = new ();
        
        private void Awake()
        {
            GameManager.Instance.OnGameStart += OnGameStartSetUp;
            
            GameManager.Instance.OnPlayerTurnStart += SetPlayerHandInteractable;
            GameManager.Instance.OnPlayerTurnStart += ShowPlayerHand;
            GameManager.Instance.OnPlayerTurnEnd += HidePlayerHand;
        }

        private void Update()
        {
            if (_isHandInteractable)
                _draggableObjectMouseInput.UpdateMouseInput();
        }
        
        private void SetPlayerHandInteractable(PlayerController playerController)
        {
            _isHandInteractable = playerController == GameManager.Instance.ClientOwnerPlayerController;
        }
        
        public PlayerDiceHand GetPlayerDiceHand(ulong clientOwnerID)
        {
            return _playerDiceHands[clientOwnerID];
        }
        
        public PlayerCardHand GetPlayerCardHand(ulong clientOwnerID)
        {
            return _playerCardHands[clientOwnerID];
        }
        
        private void OnGameStartSetUp()
        {
            foreach (var playerController in GameManager.Instance.PlayerControllers)
            {
                var playerCardHand = Instantiate(GameResourceManager.Instance.PlayerCardHandPrefab, _playerCardHandParent);
                var playerDiceHand = Instantiate(GameResourceManager.Instance.PlayerDiceHandPrefab, _playerDiceHandParent);
                
                playerDiceHand.Initialize(playerController);
                playerCardHand.Initialize(playerController);
                playerController.PlayerResourceController.InitializeHand(playerDiceHand, playerCardHand);
                
                _playerCardHands.Add(playerController.OwnerClientId, playerCardHand);
                _playerDiceHands.Add(playerController.OwnerClientId, playerDiceHand);
                
                HidePlayerHand(playerController);
            }
        }
        
        private void ShowPlayerHand(PlayerController playerController)
        {
            ShowCardHand(_playerCardHands[playerController.OwnerClientId]);
            ShowDiceHand(_playerDiceHands[playerController.OwnerClientId]);
        }
        
        private void HidePlayerHand(PlayerController playerController)
        {
            HideCardHand(_playerCardHands[playerController.OwnerClientId]);
            HideDiceHand(_playerDiceHands[playerController.OwnerClientId]);
        }

        private void ShowCardHand(PlayerCardHand playerCardHand)
        {
            playerCardHand.transform.SetParent(_playerCardHandParent);
            playerCardHand.transform.position = _playerCardHandParent.position;
        }

        private void HideCardHand(PlayerCardHand playerCardHand)
        {
            playerCardHand.transform.SetParent(_offScreenCardHandParent);
            playerCardHand.transform.position = _offScreenCardHandParent.position;
        }
        
        private void ShowDiceHand(PlayerDiceHand playerDiceHand)
        {
            playerDiceHand.transform.SetParent(_playerDiceHandParent);
            playerDiceHand.transform.position = _playerDiceHandParent.position;
        }
        
        private void HideDiceHand(PlayerDiceHand playerDiceHand)
        {
            playerDiceHand.transform.SetParent(_offScreenDiceHandParent);
            playerDiceHand.transform.position = _offScreenDiceHandParent.position;
        }
        
    }
}