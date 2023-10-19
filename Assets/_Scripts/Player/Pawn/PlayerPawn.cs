using System;
using UnityEngine;

namespace _Scripts.Player.Pawn
{
    public class PlayerPawn : PlayerEntity, ITargetee<PlayerPawn>
    {
        public void ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : PlayerEntity, ITargeter<TTargeter>
        {
            Debug.Log($"Success {targeter.ToString()}");
            if (targeter is HandDice)
            {
                Debug.Log("HandDice");
                HandDice handDice = targeter as HandDice;
                
                
                var diceRes = handDice.GetNumber();
        
                Debug.Log("Dice roll result is: "+diceRes);
                handDice.DropDice();
            }
            else
            {
                Debug.Log("Unknown");
            }
        }
    }
}