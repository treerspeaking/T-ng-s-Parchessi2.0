using System;
using System.Collections.Generic;
using _Scripts.NetworkContainter;
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
        
        [SerializeField] private HandCard _handCardPrefab;
        
        public HandCard GetHandCardPrefab()
        {
            return _handCardPrefab;
        }
    }
}