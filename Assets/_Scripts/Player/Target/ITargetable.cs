using UnityEngine;

namespace _Scripts.Player.Target
{
    public interface ITargetable
    {
        public void ExecuteTarget<T>(T dragAndDropSelection) where T : MonoBehaviour;
    }
}