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
        }
    }
}