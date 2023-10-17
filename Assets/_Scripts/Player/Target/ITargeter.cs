using UnityEngine;

public interface ITargeter<T> where T : PlayerEntity
{
    public virtual T GetTarget()
    {
        return this as T;
    }
}
