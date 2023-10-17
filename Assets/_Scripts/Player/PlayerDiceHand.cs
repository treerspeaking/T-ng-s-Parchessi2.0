using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Scriptable_Objects;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerDiceHand : PlayerControllerStartSetUpDependency
    {
        
        private List<HandDice> _handDiceList = new List<HandDice>();

        protected override void GameSetUp()
        {
            
        }

        public void AddDiceToHand(DiceContainer cardContainer)
        {
            var handDice = CreateDiceHand(cardContainer);
            
            _handDiceList.Add(handDice);
        }
        
        public void AddDiceToHand(HandDice handDice)
        {
            _handDiceList.Add(handDice);
        }
        
        public HandDice CreateDiceHand(DiceContainer diceContainer)
        {
            var diceDescription = GameResourceManager.Instance.GetDiceDescription(diceContainer.DiceID);
            var handDice = Instantiate(GameResourceManager.Instance.HandDice);
            handDice.Initialize(this, diceDescription);
            return handDice;
        }
        
    }

    
}