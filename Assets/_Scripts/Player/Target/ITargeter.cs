
using System;
using _Scripts.Simulation;
using UnityEngine;

public interface ITargeter
{
    public ulong OwnerClientID { get; set; }
    public int ContainerIndex { get; set; }
    public TargetType TargetType { get; set; }
    public SimulationPackage ExecuteTargeter<TTargetee>(TTargetee targetee) where TTargetee : ITargetee;

    public virtual MonoBehaviour GetMonoBehavior() 
    {
        return this as MonoBehaviour;
    }
    
}
