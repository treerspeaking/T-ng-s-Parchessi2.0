using _Scripts.Simulation;
using UnityEngine;

public interface ITargetee
{
    public ulong OwnerClientID { get; set; }
    public int ContainerIndex { get; set; }
    public TargetType TargetType { get; set; }
    public SimulationPackage ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : ITargeter;

    public virtual MonoBehaviour GetMonoBehavior() 
    {
        return this as MonoBehaviour;
    }
}
