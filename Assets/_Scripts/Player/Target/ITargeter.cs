using UnityEngine;

public interface ITargeter<T> where T : MonoBehaviour
{
    public virtual T GetTarget()
    {
        return this as T;
    }
}
