using System.Collections;
using System.Collections.Generic;
using _Scripts.Player.Pawn;
using _Scripts.Simulation;
using UnityEngine;

public class PawnHandCard : HandCard
{
    
    
    public virtual SimulationPackage ExecuteTargeter<TTargetee>(TTargetee targetee) where TTargetee : ITargetee
    {
        
        var package = new SimulationPackage();
        
        if (targetee is MapPawn playerPawn)
        {
            package.AddToPackage(() =>
            {
            
                // Inherit this class and write Card effect
                Debug.Log(name + " Card drag to Pawn " + playerPawn.name);
                PlayerCardHand.PlayCard(this);

                Destroy();
                
            });
        }
        
        
        return package;
    }
   
    
    
}
