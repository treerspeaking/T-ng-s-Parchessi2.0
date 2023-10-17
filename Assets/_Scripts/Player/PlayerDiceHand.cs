using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Scriptable_Objects;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerDiceHand : PlayerControllerStartSetUpDependency
    {
        
        private Dictionary<HandDice, int> _handDiceToDiceContainerDictionary = new ();

        protected override void GameSetUp()
        {
            
        }

        public void AddDiceToHand(DiceContainer diceContainer, int diceContainerIndex)
        {
            var handDice = CreateDiceHand(diceContainer);
            
            _handDiceToDiceContainerDictionary.Add(handDice, diceContainerIndex);
        }
        
        
        public HandDice CreateDiceHand(DiceContainer diceContainer)
        {
            var diceDescription = GameResourceManager.Instance.GetDiceDescription(diceContainer.DiceID);
            var handDice = Instantiate(GameResourceManager.Instance.HandDice);
            handDice.Initialize(this, diceDescription);
            return handDice;
        }

        public void PlayDice(HandDice handDice)
        {
            int diceContainerIndex = _handDiceToDiceContainerDictionary[handDice];
            _handDiceToDiceContainerDictionary.Remove(handDice);
            Destroy(handDice.gameObject);
            
            PlayerController.PlayerResourceController.RemoveDiceServerRPC(diceContainerIndex);
        }
    }

    
}