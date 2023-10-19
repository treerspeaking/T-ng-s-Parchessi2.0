using System;
using System.Collections.Generic;
using _Scripts.Managers.Network;
using _Scripts.Map;
using _Scripts.NetworkContainter;
using _Scripts.Player.Dice;
using _Scripts.Player.Pawn;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Managers.Game
{
    public class MapManager : SingletonNetworkBehavior<MapManager>
    {
        [SerializeField] private Transform _mapParent;
        [SerializeField] private List<MapPath> _mapPaths = new ();
        
        [SerializeField] private DiceCardConverter _diceCardConverter;
        private NetworkList<PawnContainer> _mapPawnContainers;
        
        private Dictionary<int, MapPawn> _containerIndexToMapPawnDictionary = new();

        private void Awake()
        {
            _mapPawnContainers = new NetworkList<PawnContainer>();
        }

        public MapPawn GetPlayerPawn(int pawnContainerIndex)
        {
            _containerIndexToMapPawnDictionary.TryGetValue(pawnContainerIndex, out var mapPawn);
            return mapPawn;
        }

        public DiceCardConverter GetDiceCardConverter()
        {
            return _diceCardConverter;
        }
        
        public void SpawnPawnToMap(PawnContainer pawnContainer, int pawnContainerIndex, ulong ownerClientId)
        {
            var mapPawn = CreateMapPawn(pawnContainer, pawnContainerIndex, ownerClientId);
            _containerIndexToMapPawnDictionary.Add(pawnContainerIndex, mapPawn);
        }
        
        
        public MapPawn CreateMapPawn(PawnContainer pawnContainer, int pawnContainerIndex, ulong ownerClientId)
        {
            var pawnDescription = GameResourceManager.Instance.GetPawnDescription(pawnContainer.PawnID);
            var mapPawn = Instantiate(GameResourceManager.Instance.MapPawn, _mapParent);
            mapPawn.Initialize(_mapPaths[(int)ownerClientId], pawnDescription,  pawnContainerIndex, ownerClientId);
            mapPawn.NetworkObject.SpawnWithOwnership(ownerClientId);
            return mapPawn;
        }

        
    }
}