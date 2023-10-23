
using _Scripts.Player.Pawn;
using UnityEditor;
using UnityEngine;

[CreateAssetMenu(fileName = "PawnDescription", menuName = "ScriptableObjects/PawnDescription", order = 1)]
public class PawnDescription : ScriptableObject
{
    public int PawnID;
    public Sprite PawnSprite;
    public int PawnMaxHealth;
    public int PawnAttackDamage;
    public int PawnMovementSpeed;

    [SerializeField] private MapPawn _mapPawnPrefab;
    
    public MapPawn GetMapPawnPrefab()
    {
        return _mapPawnPrefab;
    }
}
