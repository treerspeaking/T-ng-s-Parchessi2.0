using System;
using _Scripts.Managers.Game;
using _Scripts.Map;
using _Scripts.Simulation;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Player.Pawn
{
    public class MapPawn : NetworkBehaviour, ITargetee
    {
        
        public int ContainerIndex
        { 
            get => _containerIndex;
            set { }
        }
        public ulong ClientOwnerID 
        { 
            get => _ownerClientID;
            set { }
        }
    
        public TargetType TargetType
        {
            get => _targeteeType;
            set {}
        }

        [SerializeField] private TargetType _targeteeType;
        private ulong _ownerClientID;
        private int _containerIndex;

        private MapPath _mapPath;
        private PawnDescription _pawnDescription;

        private int _currentMapCellIndex = 0;
        private int _finalMapCellIndex = 0;

        public void Initialize(MapPath playerMapPawn, PawnDescription pawnDescription , int containerIndex, ulong ownerClientId)
        {
            _mapPath = playerMapPawn;
            _pawnDescription = pawnDescription;
            _containerIndex = containerIndex;
            _ownerClientID = ownerClientId;    
        }

        public void Move(int stepCount)
        {
            Debug.Log("Player Pawn move "+ stepCount);
            _finalMapCellIndex += stepCount;
            MapManager.Instance.UpdatePawnPositionServerRPC(_containerIndex, _finalMapCellIndex);
        }
        
        public SimulationPackage MoveAnimation(int endMapCellIndex)
        {
            _finalMapCellIndex = endMapCellIndex;
            var simulationPackage = new SimulationPackage();
            
            simulationPackage.AddToPackage(() =>
            {
                _currentMapCellIndex += endMapCellIndex;
                transform.position = _mapPath.Path[_currentMapCellIndex].transform.position; // Teleport to the end position
            });
            
            return simulationPackage;
        
         
        }


        public SimulationPackage ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : ITargeter
        {
            return null;
        }
    }
}