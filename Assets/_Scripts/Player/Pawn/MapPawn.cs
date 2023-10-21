using System;
using _Scripts.Managers.Game;
using _Scripts.Map;
using _Scripts.Simulation;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Player.Pawn
{
    public class MapPawn : PlayerEntity
    {
        private MapPath _mapPath;
        private PawnDescription _pawnDescription;

        private int _currentMapCellIndex = 0;

        public void Initialize(MapPath playerMapPawn, PawnDescription pawnDescription , int containerIndex, ulong ownerClientId)
        {
            _mapPath = playerMapPawn;
            _pawnDescription = pawnDescription;

            Initialize(containerIndex, ownerClientId);
        }

        
        
        public SimulationPackage MoveAnimation(int endMapCellIndex)
        {
            var simulationPackage = new SimulationPackage();
            
            simulationPackage.AddToPackage(() =>
            {
                _currentMapCellIndex = endMapCellIndex;
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