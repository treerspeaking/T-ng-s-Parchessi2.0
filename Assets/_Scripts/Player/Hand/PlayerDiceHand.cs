using System;
using System.Collections.Generic;
using _Scripts.Managers.Game;
using _Scripts.NetworkContainter;
using _Scripts.Player.Dice;
using _Scripts.Player.Pawn;
using _Scripts.Scriptable_Objects;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerDiceHand : PlayerControllerCompositionDependency
    {
        [SerializeField] private int _maxDices = 3;
        
        private readonly Dictionary<int, HandDice> _containerIndexToHandDiceDictionary = new Dictionary<int, HandDice>();
        private HandDiceRegion _handDiceRegion;
        private void Awake()
        {
            _handDiceRegion = gameObject.GetComponent<HandDiceRegion>();
        }

        public HandDice GetHandDice(int diceContainerIndex)
        {
            _containerIndexToHandDiceDictionary.TryGetValue(diceContainerIndex, out var handDice);
            return handDice;
        }
        

        public void AddDiceToHand(DiceContainer diceContainer, int diceContainerIndex)
        {
            if(_containerIndexToHandDiceDictionary.Count >= _maxDices ) return;
            var handDice = CreateDiceHand(diceContainer, diceContainerIndex);
            _containerIndexToHandDiceDictionary.Add(diceContainerIndex, handDice);
            
            _handDiceRegion.TryAddCard(handDice.GetComponent<HandDiceDragAndTargeter>());
        }
        
        
        public HandDice CreateDiceHand(DiceContainer diceContainer, int diceContainerIndex)
        {
            var diceDescription = GameResourceManager.Instance.GetDiceDescription(diceContainer.DiceID);
            var handDice = Instantiate(GameResourceManager.Instance.HandDicePrefab);
            handDice.Initialize(this, diceDescription,  diceContainerIndex, PlayerController.OwnerClientId);
            return handDice;
        }

        public void PlayDice(HandDice handDice, MapPawn mapPawn)
        {
            if (IsOwner)
            {
                PlayerController.PlayerResourceController.RemoveDiceServerRPC(handDice.ContainerIndex);
                MapManager.Instance.MovePawnServerRPC(mapPawn.ContainerIndex, handDice.DiceLowerRange, handDice.DiceUpperRange);

            }
            else
            {
                _handDiceRegion.RemoveCard(handDice.GetComponent<HandDiceDragAndTargeter>());
            }
            
            _containerIndexToHandDiceDictionary.Remove(handDice.ContainerIndex);
        }

        
        
        public void ConvertToCard(HandDice handDice)
        {
            if (IsOwner)
            {
                PlayerController.PlayerResourceController.RemoveDiceServerRPC(handDice.ContainerIndex);
                PlayerController.PlayerResourceController.AddCardToHandServerRPC();
            }
            else
            {
                _handDiceRegion.RemoveCard(handDice.GetComponent<HandDiceDragAndTargeter>());
            }
            
            _containerIndexToHandDiceDictionary.Remove(handDice.ContainerIndex);
        }
        
    }

    
}