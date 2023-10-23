using _Scripts.NetworkContainter;
using _Scripts.Player.Card;
using _Scripts.Player.Pawn;
using UnityEngine;

namespace _Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "PawnCardDescription", menuName = "ScriptableObjects/PawnCardDescription", order = 1)]
    public class PawnCardDescription : CardDescription
    {
        public PawnDescription PawnDescription;
        
        public PawnHandCard GetPawnHandCardPrefab()
        {
            return HandCardPrefab as PawnHandCard;
        }
        
        public override CardContainer GetCardContainer()
        {
            return new CardContainer{
                CardID = CardID,
                CardType = CardType.Pawn
            };
        }
    }
}