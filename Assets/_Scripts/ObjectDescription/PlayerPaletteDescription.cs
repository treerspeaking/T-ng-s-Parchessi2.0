using UnityEngine;

namespace _Scripts.Scriptable_Objects
{
    [CreateAssetMenu(fileName = "PlayerPaletteDescription", menuName = "ScriptableObjects/PlayerPaletteDescription", order = 1)]
    public class PlayerPaletteDescription : ScriptableObject
    {
        public Color PrimaryColor = Color.white;
        public Color OnPrimaryColor = Color.black;
        public Color SecondaryColor = Color.gray;
        public Color OnSecondaryColor = Color.black;
        public Color PrimaryOutlineColor = Color.black;
        
    }
}