
using _Scripts.Player;
using _Scripts.Player.Card;
using DG.Tweening;
using Shun_Card_System;
using UnityEngine;


public class HandCardRegion : HandDraggableObjectRegion
{
    public override bool CheckCompatibleObject(BaseDraggableObject baseDraggableObject)
    {
        return baseDraggableObject is HandCardDragAndTargeter;
    }

}
