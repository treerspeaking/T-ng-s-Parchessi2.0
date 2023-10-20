using System;
using _Scripts.Map;
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
        }


        public void ExecuteTargetee<TTargeter>(TTargeter targeter) where TTargeter : ITargeter
        {
            
        }
    }
}