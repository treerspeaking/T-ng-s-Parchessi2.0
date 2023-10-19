using System;
using _Scripts.NetworkContainter;
using _Scripts.Player.Pawn;

using UnityEngine;


[RequireComponent(typeof(PlayerEntity))]
public class TargetEntity : MonoBehaviour
{
    [SerializeField] protected TargetType TargetType;
    private void Awake()
    {
        ActionManager.Instance.AddTargetEntity(this);   
        
    }
    
    public TargetType GetTargetType()
    {
        return TargetType;

    }
    
    public virtual T Get<T>() where T : PlayerEntity , ITargetee<T>
    {
        return GetComponent<T>();
    }
    
}
