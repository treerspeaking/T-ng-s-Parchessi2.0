using System;
using System.Collections.Generic;
using _Scripts.DataWrapper;
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
        protected MapManager MapManager => MapManager.Instance;
        private MapPath _mapPath;
        private PawnDescription _pawnDescription;

        public int StandingMapCellIndex = 0;

        public ObservableData<int> AttackDamage;
        public ObservableData<int> MaxHealth;
        public ObservableData<int> CurrentHealth;
        public ObservableData<int> MovementSpeed;

        public void Initialize(MapPath playerMapPawn, PawnDescription pawnDescription , int containerIndex, ulong ownerClientId)
        {
            _mapPath = playerMapPawn;
            _pawnDescription = pawnDescription;

            Initialize(containerIndex, ownerClientId);
            LoadPawnDescription();
        }

        public void LoadPawnDescription()
        {
            AttackDamage = new ObservableData<int>(_pawnDescription.PawnAttackDamage);
            MaxHealth = new ObservableData<int>(_pawnDescription.PawnMaxHealth);
            CurrentHealth = new ObservableData<int>(_pawnDescription.PawnMaxHealth);
            MovementSpeed = new ObservableData<int>(_pawnDescription.PawnMovementSpeed);
        }

        

        private bool TryMove(int startMapCellIndex, int stepCount)
        {
            bool isMovable = true;
            int endMapCellIndex = stepCount + startMapCellIndex;
                
            for (var index = startMapCellIndex; index <= stepCount + startMapCellIndex; index++)
            {
                var mapCell = _mapPath.Path[index];

                if (mapCell.CheckEnterable()) continue;
                    
                isMovable = false;
                break;

            }
                
            if (isMovable && _mapPath.Path[endMapCellIndex].CheckEnterable()) 
                // If the last cell , try to make combat to find empty slot
            {
                isMovable = !TryMakeCombatToFindEmptySlot(this, _mapPath.Path[endMapCellIndex]);
                    
            }

            return isMovable;
        }

        private bool TryMakeCombatToFindEmptySlot(MapPawn attacker, MapCell mapCell)
        {
            bool emptySlot = mapCell.CheckEnterable();
            var defenders = mapCell.GetAllPawn();
            foreach (var defender in defenders)
            {
                int damage = attacker.AttackDamage.Value;
                int currentHealth = defender.CurrentHealth.Value;
                
                if (currentHealth - damage <= 0)
                {
                    emptySlot = true; 
                }
            }

            return emptySlot; // If there is an empty slot, the attacker can move to that slot
        }
        
        public SimulationPackage StartMove(int startMapCellIndex, int stepCount)
        {
            var simulationPackage = new SimulationPackage();
            
            simulationPackage.AddToPackage(() =>
            {
                // Teleport to the end position
                //transform.position = _mapPath.Path[_standingMapCellIndex].transform.position;
                
                if(TryMove(startMapCellIndex, stepCount))
                {
                    var endMapCellIndex = stepCount + startMapCellIndex;

                    foreach (var mapPawn in _mapPath.Path[endMapCellIndex].GetAllPawn())
                    {
                        MapManager.MakeCombatServerRPC(ContainerIndex, mapPawn.ContainerIndex);
                    }
                    
                    MapManager.EndMovePawnServerRPC(ContainerIndex, endMapCellIndex);
                }
                else
                {
                    
                }
            });
            
            return simulationPackage;
         
        }

        public SimulationPackage EndMove(int endMapCellIndex)
        {
            
            var simulationPackage = new SimulationPackage();
            
            simulationPackage.AddToPackage(() =>
            {
                // Teleport to the end position
                StandingMapCellIndex = endMapCellIndex;
                transform.position = _mapPath.Path[StandingMapCellIndex].transform.position; 
            });
            
            return simulationPackage;

        }
        

        public SimulationPackage ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : ITargeter
        {
            return null;
        }


        public SimulationPackage MakeCombat(MapPawn defenderMapPawn)
        {
            throw new NotImplementedException();
        }
    }
}