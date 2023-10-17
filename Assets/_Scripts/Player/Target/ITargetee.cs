using UnityEngine;

namespace _Scripts.Player.Target
{
    public interface ITargetee<T> where T : MonoBehaviour
    {
        public void ExecuteTarget<TTargeter>(TTargeter targeter) where TTargeter : MonoBehaviour, ITargeter<TTargeter>;
    }
}