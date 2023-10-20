using System;
using System.Collections;

using _Scripts.Player;
using _Scripts.Player.Dice;
using _Scripts.Player.Pawn;
using _Scripts.Simulation;
using Shun_Card_System;
using Shun_Unity_Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HandDice : PlayerEntity, ITargeter
{

    public Action OnTargeterDestroy { get; set; }
    private PlayerDiceHand _playerDiceHand;
    [SerializeField, ShowImmutable] DiceDescription _diceDescription;
    private ITargeter _targeterImplementation;
 
    
    public void Initialize(PlayerDiceHand playerDiceHand, DiceDescription diceDescription, int containerIndex, ulong ownerClientID )
    {
        _playerDiceHand = playerDiceHand;
        _diceDescription = diceDescription;
        base.Initialize(containerIndex, ownerClientID);
    }

    protected virtual int GetNumber()
    {
        var random = new System.Random();
        return random.Next(_diceDescription.DiceLowerRange, _diceDescription.DiceUpperRange);
    }



    public virtual CoroutineSimulationPackage ExecuteTargeter<TTargetee>(TTargetee targetee) where TTargetee : ITargetee
    {
        var package = new CoroutineSimulationPackage();
        package.AddToPackage(() =>
        {
            if (targetee is MapPawn playerPawn)
            {

                playerPawn.Move(GetNumber());
                _playerDiceHand.PlayDice(this);

                // Inherit this class and write Dice effect
                Debug.Log(name + " Dice drag to Pawn " + playerPawn.name);

            }
            else if (targetee is PlayerDeck)
            {
                Debug.Log("Draw a card");
                _playerDiceHand.ConvertToCard(this);
                
            }

            if (TryGetComponent<BaseDraggableObject>(out var baseDraggableObject))
                baseDraggableObject.OnDestroy();
            Destroy(gameObject);
        });
        
        return package;
    }

}
