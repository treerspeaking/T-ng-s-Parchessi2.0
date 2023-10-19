using UnityEngine;

public interface ITargetee
{
    public ulong ClientOwnerID { get; set; }
    public int ContainerIndex { get; set; }
    public TargetType TargetType { get; set; }
    public void ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : ITargeter;

    public virtual MonoBehaviour GetMonoBehavior() 
    {
        return this as MonoBehaviour;
    }
}
