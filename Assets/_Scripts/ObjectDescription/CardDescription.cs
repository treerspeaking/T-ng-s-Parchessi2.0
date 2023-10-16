using UnityEngine;

namespace _Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "CardDescription", menuName = "ScriptableObjects/CardDescription", order = 1)]
    public class CardDescription : ScriptableObject
    {
        public int CardID;
        public Sprite CardSprite;
        public string CardName;
        public string CardDescriptionText;
        public int CardCost;
        
    }
}