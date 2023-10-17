using UnityEditor;
using UnityEngine;


[CreateAssetMenu(fileName = "DiceDescription", menuName = "ScriptableObjects/DiceDescription", order = 1)]
public class DiceDescription : ScriptableObject
{
    public int DiceID;
    public string DiceAssetPath => AssetDatabase.GetAssetPath(this);
    public int DiceLowerRange;
    public int DiceUpperRange;

}
