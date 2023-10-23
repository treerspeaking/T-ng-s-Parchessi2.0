
using System;
using System.Threading.Tasks;
using _Scripts.Player.Dice;
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



    public virtual SimulationPackage ExecuteTargeter<TTargetee>(TTargetee targetee) where TTargetee : ITargetee
    {
        
        var package = new SimulationPackage();
        
        
        if (targetee is MapPawn playerPawn)
        {
            package.AddToPackage(() =>
            {
            
                // Inherit this class and write Card effect
                Debug.Log(name + " Card drag to Pawn " + playerPawn.name);
                _playerCardHand.PlayCard(this);

                Destroy();
                
            });
        }
        
        
        return package;
    }

    public SimulationPackage Discard()
    {
        var package = new SimulationPackage();
        package.AddToPackage(() =>
        {
            Debug.Log("Discard Card");
            Destroy();
        });
        return package;
    }

    private void Destroy()
    {
        if (TryGetComponent<BaseDraggableObject>(out var baseDraggableObject))
            baseDraggableObject.Destroy();
        Destroy(gameObject);
    }
}
