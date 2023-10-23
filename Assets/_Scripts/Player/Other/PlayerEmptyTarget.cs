using _Scripts.Simulation;
using UnityEngine;

namespace _Scripts.Player.Dice
{
    public class PlayerEmptyTarget : MonoBehaviour , ITargetee
    {
        [SerializeField] private TargetType _targeteeType;

        public ulong OwnerClientID { get; set; }
        public int ContainerIndex { get; set; }
        public TargetType TargetType { get; set; }
        
        public SimulationPackage ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : ITargeter
        {
            return null;
        }

        public void StartHighlight()
        {
            
        }

        public void EndHighlight()
        {
            
        }
    }
}