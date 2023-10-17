using UnityEngine;

namespace _Scripts.Player.Target
{
    public interface ITargetee<T> where T : PlayerEntity
    {
        public void ExecuteTarget<TTargeter>(TTargeter targeter) where TTargeter : PlayerEntity, ITargeter<TTargeter>;
        
        public virtual T GetTarget()
        {
            return this as T;
        }
    }
}