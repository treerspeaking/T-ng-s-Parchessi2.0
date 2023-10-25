using _Scripts.Player.Card;
using _Scripts.Scriptable_Objects;
using Shun_Card_System;

namespace _Scripts.Player.Dice
{
    public class HandDiceRegion : HandDraggableObjectRegion
    {
        
        public override bool CheckCompatibleObject(BaseDraggableObject baseDraggableObject)
        {
            return baseDraggableObject is HandDiceDragAndTargeter;
        }

    }
}