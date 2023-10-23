using _Scripts.Player.Pawn;
using UnityEngine;

namespace _Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "PawnCardDescription", menuName = "ScriptableObjects/PawnCardDescription", order = 1)]
    public class PawnCardDescription : CardDescription
    {
        public PawnDescription PawnDescription;
        
        [SerializeField] private PawnHandCard _mapPawnPrefab;
        
        public PawnHandCard GetPawnHandCardPrefab()
        {
            return _mapPawnPrefab;
        }
    }
}