using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HandDice : MonoBehaviour
{
    DiceDescription _diceDescription;
    
    public void Initialize(DiceDescription diceDescription)
    {
        _diceDescription = diceDescription;
    }
    private int GetDice()
    {
        if (_diceDescription == null) return -1;
        
        return Random.Range(_diceDescription.DiceLowerRange, _diceDescription.DiceUpperRange);
    }
    
    public TextMeshProUGUI textMeshPro;
    
    public void PrintDice()
    {
        
        var diceRes = this.GetDice().ToString();
        
        textMeshPro.text = diceRes;
        Debug.Log("Dice roll result is: "+diceRes);
    }

}
