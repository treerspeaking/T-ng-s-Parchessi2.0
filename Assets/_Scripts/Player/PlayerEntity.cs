using System;
using UnityEngine;

public class PlayerEntity : MonoBehaviour, ITargetee<PlayerEntity>
{
    public int ContainerIndex { get; protected set; }
    public ulong OwnerClientID { get; protected set; }

    public Action OnDestroy;
    
    public void Initialize(int containerIndex, ulong ownerClientID)
    {
        ContainerIndex = containerIndex;
        OwnerClientID = ownerClientID;
    }

    public virtual void ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : PlayerEntity, ITargeter<TTargeter>
    {
        
    }

    protected void Destroy()
    {
        OnDestroy.Invoke();
        Destroy(gameObject);
    }
}