
using UnityEngine;

[CreateAssetMenu(fileName = "PawnDescription", menuName = "ScriptableObjects/PawnDescription", order = 1)]
public class PawnDescription : ScriptableObject
{
    public int PawnID;
    public Sprite PawnSprite;
    public int PawnMaxHealth;
    public int PawnAttack;
    
    
}
