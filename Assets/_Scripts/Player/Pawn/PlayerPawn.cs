using System;
using UnityEngine;

namespace _Scripts.Player.Pawn
{
    public class PlayerPawn : PlayerEntity, ITargetee<PlayerPawn>
    {
        public void Move(int stepCount)
        {
            Debug.Log("Player Pawn move "+ stepCount);
        }

        public override void ExecuteTargetee<TTargeter>(TTargeter targeter)
        {
            
        }
    }
}