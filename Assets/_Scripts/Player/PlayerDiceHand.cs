using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Scriptable_Objects;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerDiceHand : PlayerControllerStartSetUpDependency
    {

        protected override void GameSetUp()
        {
            
        }

        public void AddDiceToHand(DiceContainer diceContainer, int diceContainerIndex)
        {
            var handDice = CreateDiceHand(diceContainer, diceContainerIndex);
            
        }
        
        
        public HandDice CreateDiceHand(DiceContainer diceContainer, int diceContainerIndex)
        {
            var diceDescription = GameResourceManager.Instance.GetDiceDescription(diceContainer.DiceID);
            var handDice = Instantiate(GameResourceManager.Instance.HandDice);
            handDice.Initialize(this, diceContainerIndex, PlayerController.OwnerClientId, diceDescription);
            return handDice;
        }

        public void PlayDice(HandDice handDice)
        {
            Destroy(handDice.gameObject);
            
            PlayerController.PlayerResourceController.RemoveDiceServerRPC(handDice.ContainerIndex);
        }
    }

    
}