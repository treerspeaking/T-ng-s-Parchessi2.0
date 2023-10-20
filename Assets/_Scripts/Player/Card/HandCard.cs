
using System;
using System.Threading.Tasks;
using _Scripts.Player.Pawn;
using _Scripts.Scriptable_Objects;
using _Scripts.Simulation;
using Shun_Card_System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HandCard : PlayerEntity, ITargeter
{
    private PlayerCardHand _playerCardHand;
    public CardDescription CardDescription { get; protected set; }

    public Action OnTargeterDestroy { get; set; }

    public void Initialize(PlayerCardHand playerCardHand, CardDescription cardDescription, int containerIndex, ulong ownerClientID)
    {
        _playerCardHand = playerCardHand;
        CardDescription = cardDescription;
        Initialize(containerIndex, ownerClientID);
    }



    public virtual CoroutineSimulationPackage ExecuteTargeter<TTargetee>(TTargetee targetee) where TTargetee : ITargetee
    {
        if (targetee is not MapPawn playerPawn)
        {
            Debug.LogError("Card drag to not Pawn");
            return null;
        }
        
        
        var package = new CoroutineSimulationPackage();
        package.AddToPackage(() =>
        {
            // Inherit this class and write Card effect
            Debug.Log(name + " Card drag to Pawn " + playerPawn.name);
            _playerCardHand.PlayCard(this);
            
            if( TryGetComponent<BaseDraggableObject>(out var baseDraggableObject))
                baseDraggableObject.OnDestroy();
            Destroy(gameObject);
            return null;
        });
        
        return package;
    }

}
