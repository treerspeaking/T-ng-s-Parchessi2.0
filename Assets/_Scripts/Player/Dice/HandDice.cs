using System.Collections;

using System.Collections.Generic;
using _Scripts.Player;
using Shun_Unity_Editor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HandDice : PlayerEntity, ITargeter<HandDice>
{

    private PlayerDiceHand _playerDiceHand;
    [SerializeField, ShowImmutable] DiceDescription _diceDescription;
    private ITargeter<HandDice> _targeterImplementation;
 
    
    public void Initialize(PlayerDiceHand playerDiceHand, int containerIndex, ulong ownerClientID , DiceDescription diceDescription)
    {
        _playerDiceHand = playerDiceHand;
        _diceDescription = diceDescription;
        Initialize(containerIndex, ownerClientID);
    }

    public int GetNumber()
    {
        return Random.Range(_diceDescription.DiceLowerRange, _diceDescription.DiceUpperRange);
    }
    
    public void DropDice()
    {
        
        _playerDiceHand.PlayDice(this);
    }

    public virtual void ExecuteTargeter<TTargetee>(TTargetee targeter) where TTargetee : PlayerEntity
    {
        
        
    }
}
