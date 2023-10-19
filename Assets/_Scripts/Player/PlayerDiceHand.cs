using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Scriptable_Objects;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerDiceHand : PlayerControllerStartSetUpDependency
    {
        private readonly Dictionary<int, HandDice> _containerIndexToHandDiceDictionary = new Dictionary<int, HandDice>();
        protected override void GameSetUp()
        {
            
        }

        public HandDice GetHandDice(int diceContainerIndex)
        {
            _containerIndexToHandDiceDictionary.TryGetValue(diceContainerIndex, out var handDice);
            return handDice;
        }
        

        public void AddDiceToHand(DiceContainer diceContainer, int diceContainerIndex)
        {
            var handDice = CreateDiceHand(diceContainer, diceContainerIndex);
            _containerIndexToHandDiceDictionary.Add(diceContainerIndex, handDice);
        }
        
        
        public HandDice CreateDiceHand(DiceContainer diceContainer, int diceContainerIndex)
        {
            var diceDescription = GameResourceManager.Instance.GetDiceDescription(diceContainer.DiceID);
            var handDice = Instantiate(GameResourceManager.Instance.HandDice);
            handDice.Initialize(this, diceDescription,  diceContainerIndex, PlayerController.OwnerClientId);
            return handDice;
        }

        public void PlayDice(HandDice handDice)
        {
            PlayerController.PlayerResourceController.RemoveDiceServerRPC(handDice.ContainerIndex);
        }
    }

    
}