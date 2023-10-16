using System.Collections;

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class Dice : MonoBehaviour
{
    private int GetDice()
    {
        return Random.Range(1,6);
    }
    
    public TextMeshProUGUI textMeshPro;
    
    public void PrintDice()
    {
        
        var diceRes = this.GetDice().ToString();
        
        textMeshPro.text = diceRes;
        Debug.Log("Dice roll result is: "+diceRes);
    }
    
}
