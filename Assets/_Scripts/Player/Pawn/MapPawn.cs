using System;
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

        private int _currentMapCellIndex = 0;

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

        public SimulationPackage StartMove(int startMapCellIndex, int stepCount)
        {
            var simulationPackage = new SimulationPackage();
            
            simulationPackage.AddToPackage(() =>
            {
                // Teleport to the end position
                //transform.position = _mapPath.Path[_currentMapCellIndex].transform.position;
                
                for (var index = startMapCellIndex; index < stepCount + startMapCellIndex; index++)
                {
                    var mapCell = _mapPath.Path[index];
                    
                    _mapPath.Path[startMapCellIndex].RemovePawn(this);
                    
                    if (mapCell.CheckEnterable())
                    {
                        var allPawn = mapCell.GetAllPawn();
                        if (allPawn.Count > 0)
                        {
                            foreach (var pawn in allPawn)
                            {
                                MapManager.MakeCombat(this, pawn);
                            }
                        
                            mapCell.EnterPawn(this);    
                        }
                        else
                        {
                            
                        }
                        
                    }
                    else
                    {
                        break;
                    }
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
                _currentMapCellIndex = endMapCellIndex;
                transform.position = _mapPath.Path[_currentMapCellIndex].transform.position; 
            });
            
            return simulationPackage;

        }
        

        public SimulationPackage ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : ITargeter
        {
            return null;
        }
        
        
        
    }
}