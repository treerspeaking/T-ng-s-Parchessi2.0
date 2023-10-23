using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers.Game;
using _Scripts.Player.Dice;
using _Scripts.Player.Pawn;
using _Scripts.Scriptable_Objects;
using _Scripts.Simulation;
using UnityEngine;

public class PawnHandCard : HandCard
{
    protected PawnDescription PawnDescription;

    
    protected override void InitializeCardDescription(CardDescription cardDescription)
    {
        base.InitializeCardDescription(cardDescription);
        var pawnCardDescription = cardDescription as PawnCardDescription;
        
        if (pawnCardDescription != null) 
            PawnDescription = pawnCardDescription.PawnDescription;
        else 
            Debug.LogError("PawnCardDescription is null");
    }

    public override SimulationPackage ExecuteTargeter<TTargetee>(TTargetee targetee)
    {
        
        var package = new SimulationPackage();
        
        if (targetee is PlayerEmptyTarget playerEmptyTarget && FindObjectOfType<PawnHandCard>() != null)
        {
            package.AddToPackage(() =>
            {
            
                // Inherit this class and write Card effect
                Debug.Log(name + " Card drag to Empty ");
                PlayerCardHand.PlayCard(this);

                MapManager.Instance.SpawnPawnToMap(PawnDescription, OwnerClientID);
                
                Destroy();
                
            });
        }
        else if (targetee is MapPawn playerPawn)
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
