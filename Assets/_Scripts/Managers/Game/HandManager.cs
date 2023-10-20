using System;
using System.Collections.Generic;
using _Scripts.Player;
using Shun_Unity_Editor;
using UnityEngine;
using UnityUtilities;

namespace _Scripts.Managers.Game
{
    public class HandManager : SingletonMonoBehaviour<HandManager>
    {
        private Dictionary<ulong,PlayerCardHand> _playerCardHands = new ();
        private Dictionary<ulong,PlayerDiceHand> _playerDiceHands = new ();

        [SerializeField] private Transform _playerCardHandParent;
        [SerializeField] private Transform _playerDiceHandParent;
        
        private void Awake()
        {
            GameManager.Instance.OnGameStart += OnGameStartSetUp;
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
            }
        }
        
        
    }
}