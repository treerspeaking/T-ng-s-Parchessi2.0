using System.Collections.Generic;
using _Scripts.NetworkContainter;
using UnityEngine;

namespace _Scripts.Player
{
    public class PlayerDiceHand : PlayerControllerStartSetUpDependency
    {
        private List<HandDice> _handDiceList = new List<HandDice>();

        protected override void GameSetUp()
        {
            
        }
        
        
        public void AddDiceToHand(HandDice handDice)
        {
            _handDiceList.Add(handDice);
        }
        
        public void CreateDiceHand(CardContainer cardContainer)
        {
            var diceDescription = GameResourceManager.Instance.GetDiceDescription(cardContainer.CardID);
            var handDice = Instantiate(GameResourceManager.Instance.HandDice);
            handDice.InitializeDice(diceDescription);
            _handDiceList.Add(handDice);
        }
        
    }

    
}