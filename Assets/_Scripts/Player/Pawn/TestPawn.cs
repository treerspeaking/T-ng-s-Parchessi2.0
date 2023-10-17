using System;
using _Scripts.Player.Target;
using UnityEngine;

namespace _Scripts.Player.Pawn
{
    public class TestPawn : PlayerEntity, ITargetee<TestPawn>
    {
        public void ExecuteTarget<TTargeter>(TTargeter targeter) where TTargeter : PlayerEntity, ITargeter<TTargeter>
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