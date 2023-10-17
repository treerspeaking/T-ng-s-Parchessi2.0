using System;
using _Scripts.Player.Target;
using UnityEngine;

namespace _Scripts.Player.Pawn
{
    public class TestPawn : MonoBehaviour, ITargetee<TestPawn>
    {
        public void ExecuteTarget<TTargeter>(TTargeter targeter) where TTargeter : MonoBehaviour, ITargeter<TTargeter>
        {
            Debug.Log($"Success {targeter.ToString()}");
            if (targeter is HandDice)
            {
                Debug.Log("HandDice");
                HandDice handDice = targeter as HandDice;
                handDice.PrintDice();
            }
            else
            {
                Debug.Log("Unknown");
            }
        }
    }
}