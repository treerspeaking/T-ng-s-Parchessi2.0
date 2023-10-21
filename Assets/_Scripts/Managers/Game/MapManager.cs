using System;
using System.Collections.Generic;
using System.Linq;
using _Scripts.Managers.Network;
using _Scripts.Map;
using _Scripts.NetworkContainter;
using _Scripts.Player.Dice;
using _Scripts.Player.Pawn;
using _Scripts.Simulation;
using QFSW.QC;
using Unity.Netcode;
using UnityEngine;

namespace _Scripts.Managers.Game
{
    public class MapManager : SingletonNetworkBehavior<MapManager>
    {
        private const int MAP_PAWN_COUNT = 20;
        private static readonly PawnContainer EmptyPawnContainer = new PawnContainer{PawnID = -1};
        
        [SerializeField] private Transform _mapParent;
        [SerializeField] private List<MapPath> _mapPaths = new ();
        
        [SerializeField] private PlayerDeck _playerDeck;
        
        private NetworkList<PawnContainer> _mapPawnContainers;
        
        private Dictionary<int, MapPawn> _containerIndexToMapPawnDictionary = new();

        
        private void Awake()
        {
            _mapPawnContainers = new NetworkList<PawnContainer>(Enumerable.Repeat(EmptyPawnContainer, MAP_PAWN_COUNT).ToArray());
        }
        
        [Command()]
        public void PlayerSpawnPawnToMapServer( )
        {
            SpawnPawnToMapServerRPC(new PawnContainer{PawnID = 0, ClientOwnerID = NetworkManager.LocalClientId, StandingMapCell = 0, StandingMapSpot = 0}, NetworkManager.LocalClientId);
        }
        

        public MapPawn GetPlayerPawn(int pawnContainerIndex)
        {
            _containerIndexToMapPawnDictionary.TryGetValue(pawnContainerIndex, out var mapPawn);
            return mapPawn;
        }

        public PlayerDeck GetDiceCardConverter()
        {
            return _playerDeck;
        }

        public MapPawn CreateMapPawn(PawnContainer pawnContainer, int pawnContainerIndex, ulong ownerClientId)
        {
            var pawnDescription = GameResourceManager.Instance.GetPawnDescription(pawnContainer.PawnID);
            Transform spawnTransform = _mapPaths[(int)ownerClientId].Path[0].transform;
            var mapPawn = Instantiate(GameResourceManager.Instance.MapPawnPrefab, spawnTransform.position, spawnTransform.rotation, _mapParent);
            
            mapPawn.Initialize(_mapPaths[(int)ownerClientId], pawnDescription,  pawnContainerIndex, ownerClientId);
            
            return mapPawn;
        }


        [ServerRpc(RequireOwnership = false)]
        public void SpawnPawnToMapServerRPC(PawnContainer pawnContainer, ulong ownerClientId)
        {
            
            for (var index = 0; index < _mapPawnContainers.Count; index++)
            {
                var mapPawnContainer = _mapPawnContainers[index];
                if (mapPawnContainer.Equals(EmptyPawnContainer))
                {
                    _mapPawnContainers[index] = pawnContainer;
                    SpawnPawnToMapClientRPC(pawnContainer, index, ownerClientId);
                    break;
                }
            }
        }

        [ClientRpc]
        public void SpawnPawnToMapClientRPC(PawnContainer pawnContainer, int containerIndex, ulong ownerClientId)
        {
            var mapPawn = CreateMapPawn(pawnContainer, containerIndex, ownerClientId);
            _containerIndexToMapPawnDictionary.Add(containerIndex, mapPawn);
        }
        
        [ServerRpc(RequireOwnership = false)]
        public void RemovePawnFromMapServerRPC(int pawnContainerIndex)
        {
            _containerIndexToMapPawnDictionary.Remove(pawnContainerIndex);
            _mapPawnContainers[pawnContainerIndex] = EmptyPawnContainer;
        }
        

        [ServerRpc(RequireOwnership = false)]
        public void UpdatePawnPositionServerRPC(int pawnContainerIndex, int finalMapCellIndex)
        {
            var mapPawnContainer = _mapPawnContainers[pawnContainerIndex];
            
            mapPawnContainer.StandingMapCell += finalMapCellIndex;
            
            _mapPawnContainers[pawnContainerIndex] = mapPawnContainer;
            UpdatePawnPositionClientRPC(pawnContainerIndex, finalMapCellIndex);
        }

        [ClientRpc]
        private void UpdatePawnPositionClientRPC(int pawnContainerIndex, int finalMapCellIndex)
        {
            var mapPawn = GetPlayerPawn(pawnContainerIndex);
            SimulationManager.Instance.AddCoroutineSimulationObject(mapPawn.MoveAnimation(finalMapCellIndex));
            
        } 
        
    }
}