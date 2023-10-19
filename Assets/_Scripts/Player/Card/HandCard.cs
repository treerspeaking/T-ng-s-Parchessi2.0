
using System;
using _Scripts.Player.Pawn;
using _Scripts.Scriptable_Objects;
using Shun_Card_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandCard : PlayerEntity, ITargeter<HandCard>
{
    private PlayerCardHand _playerCardHand;
    public CardDescription CardDescription { get; protected set; }


    public void Initialize(PlayerCardHand playerCardHand, CardDescription cardDescription)
    {
        _playerCardHand = playerCardHand;
        CardDescription = cardDescription;
    }


    public virtual void ExecuteTargeter<TTargetee>(TTargetee targetee) where TTargetee : PlayerEntity
    {
        if (targetee is not PlayerPawn playerPawn)
        {
            Debug.LogError("Card drag to not Pawn");
            return;
        }
        
        // Inherit this class and write Card effect
        Debug.Log(name + " Card drag to Pawn " + playerPawn.name);
        
        Destroy();
    }

}
