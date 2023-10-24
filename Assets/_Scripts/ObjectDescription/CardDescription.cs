using System;
using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Player.Card;
using UnityEditor;
using UnityEngine;

namespace _Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "CardDescription", menuName = "ScriptableObjects/CardDescription", order = 1)]
    public class CardDescription : ScriptableObject
    {
        public int CardID;
        public Sprite CardSprite;
        public string CardName;
        public string CardEffectDescription;
        public int CardCost;
        public int [] CardEffectIntVariables;
        
        public CardPaletteDescription CardPaletteDescription;
        [SerializeField] protected HandCard HandCardPrefab;

        
        
        public HandCard GetHandCardPrefab()
        {
            return HandCardPrefab;
        }
        
        public virtual CardContainer GetCardContainer()
        {
            return new CardContainer{
                CardID = CardID,
                CardType = CardType.Action
                
            };
        }
    }
}