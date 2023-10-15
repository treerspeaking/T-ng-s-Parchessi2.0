using Unity.Collections;
using Unity.Netcode;

namespace _Scripts.Player
{
    public class PlayerActionController : PlayerControllerDependency
    {
        public NativeList<int> DeckCards = new NativeList<int>();
        public NativeList<int> HandCards = new NativeList<int>();
        public NativeList<int> DiscardCards = new NativeList<int>();
        
        
    }
}