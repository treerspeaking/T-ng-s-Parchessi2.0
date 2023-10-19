using System;
using UnityEngine;

public class PlayerEntity : MonoBehaviour, ITargetee
{
    public int ContainerIndex
    { 
        get => _containerIndex;
        set { }
    }
    public ulong ClientOwnerID 
    { 
        get => _ownerClientID;
        set { }
    }
    
    public TargetType TargetType
    {
        get => _targeteeType;
        set {}
    }

    [SerializeField] private TargetType _targeteeType;
    private ulong _ownerClientID;
    private int _containerIndex;


    protected virtual void Initialize(int containerIndex, ulong ownerClientID)
    {
        _containerIndex = containerIndex;
        _ownerClientID = ownerClientID;
    }

    public virtual void ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : ITargeter
    {
        
    }

}