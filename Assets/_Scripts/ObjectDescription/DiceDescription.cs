using _Scripts.NetworkContainter;
using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "DiceDescription", menuName = "ScriptableObjects/DiceDescription", order = 1)]
public class DiceDescription : ScriptableObject
{
    public int DiceID;
    
    public int DiceLowerRange;
    public int DiceUpperRange;

    [SerializeField] private HandDice _handDicePrefab;
    
    public HandDice GetHandDicePrefab()
    {
        return _handDicePrefab;
    }
    
    public DiceContainer GetDiceContainer()
    {
        return new DiceContainer{
            DiceID = DiceID,
        };
    }
    
}
