using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.NetworkContainter;
using _Scripts.Player.Pawn;
using UnityEngine;


public abstract class DropTargetEntity<T> : TargetEntity where T : PlayerEntity, ITargetee<T>
{
    
}
