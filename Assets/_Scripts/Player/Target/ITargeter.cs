
using System;
using UnityEngine;

public interface ITargeter
{
    public ulong ClientOwnerID { get; set; }
    public int ContainerIndex { get; set; }
    public TargetType TargetType { get; set; }
    public void ExecuteTargeter<TTargetee>(TTargetee targetee) where TTargetee : ITargetee;

    public virtual MonoBehaviour GetMonoBehavior() 
    {
        return this as MonoBehaviour;
    }
    
}
