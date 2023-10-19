using UnityEngine;

public interface ITargetee<T> where T : PlayerEntity
{
    public void ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : PlayerEntity, ITargeter<TTargeter>;

    public virtual T GetTarget()
    {
        return this as T;
    }
}
