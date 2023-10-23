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

        

        public virtual bool TryMove(int startMapCellIndex, int stepCount)
        {
            
            int endMapCellIndex = stepCount + startMapCellIndex;

            if (startMapCellIndex + stepCount >= _mapPath.Path.Count) return false;
                
            for (var index = startMapCellIndex + 1; index < stepCount + startMapCellIndex; index++)
            {
                var mapCell = _mapPath.Path[index];

                if (index < _mapPath.Path.Count && mapCell.CheckEnterable()) continue;

                return false;

            }
            
            return TryMakeCombatToFindEmptySlot(this, _mapPath.Path[endMapCellIndex]);
        }

        protected virtual bool TryMakeCombatToFindEmptySlot(MapPawn attacker, MapCell mapCell)
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

            if (TryMove(startMapCellIndex, stepCount))
            {
                
                simulationPackage.AddToPackage(()=> 
                {
                    // Start move
                    _mapPath.Path[startMapCellIndex].RemovePawn(this);
                    
                    for (int step = 1; step < stepCount; step++)
                    {
                    
                        // Teleport to the end position
                        StandingMapCellIndex = step + startMapCellIndex;
                        transform.position = _mapPath.Path[StandingMapCellIndex].GetEmptySpot().transform.position; 
                    }

                    StandingMapCellIndex ++;
                });

  
                simulationPackage.AddToPackage(() =>
                {
                    // Make combat to all pawn in the cell
                    foreach (var mapPawn in _mapPath.Path[StandingMapCellIndex].GetAllPawn())
                    {
                        MapManager.MakeCombatServerRPC(ContainerIndex, mapPawn.ContainerIndex);
                    }
                    
                });
                
                simulationPackage.AddToPackage(() =>
                {
                    // End move
                    MapManager.EndMovePawnServerRPC(ContainerIndex, StandingMapCellIndex);
                });
            }
            else
            {
                
            }

            return simulationPackage;
         
        }
        
        

        public SimulationPackage EndMove(int endMapCellIndex)
        {
            
            var simulationPackage = new SimulationPackage();
            
            simulationPackage.AddToPackage(() =>
            {
                // Teleport to the end position
                StandingMapCellIndex = endMapCellIndex;
                transform.position = _mapPath.Path[endMapCellIndex].transform.position; 
                
                // End move
                _mapPath.Path[endMapCellIndex].EnterPawn(this);
            });
            
            return simulationPackage;

        }
        

        public SimulationPackage ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : ITargeter
        {
            return null;
        }


        public SimulationPackage Attack(MapPawn defenderMapPawn)
        {
            var simulationPacket = new SimulationPackage();
            
            simulationPacket.AddToPackage(() =>
            {
                // Buff from attacker and debuff from defender
                
            });
            
            return null;
        }

        public SimulationPackage Defend(MapPawn attackerMapPawn)
        {
            var simulationPacket = new SimulationPackage();
            
            simulationPacket.AddToPackage(() =>
            {
                CurrentHealth.Value -= attackerMapPawn.AttackDamage.Value;
                
                if (CurrentHealth.Value <= 0)
                {
                    // Death
                    MapManager.RemovePawnFromMapServerRPC(ContainerIndex);
                    
                    _mapPath.Path[StandingMapCellIndex].RemovePawn(this);
                }
            });
            
            return simulationPacket;
        }
        
        
        public SimulationPackage Death()
        {
            var simulationPacket = new SimulationPackage();
            
            simulationPacket.AddToPackage(() =>
            {
                // Death Animation
                Destroy(gameObject);
            });
            
            return simulationPacket;
        }
        
    }
}