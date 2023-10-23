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
        [SerializeField] private PlayerEmptyTarget _playerEmptyTarget;
        
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

        public PlayerDeck GetDeck()
        {
            return _playerDeck;
        }
        
        public ITargetee GetEmptyTarget()
        {
            return _playerEmptyTarget;
        }

        public void SpawnPawnToMap(PawnDescription pawnDescription, ulong ownerClientId)
        {
            if (ownerClientId != NetworkManager.LocalClientId) return;
            SpawnPawnToMapServerRPC(new PawnContainer{PawnID = pawnDescription.PawnID, ClientOwnerID = ownerClientId, StandingMapCell = 0, StandingMapSpot = 0}, ownerClientId);
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
            if (ownerClientId != NetworkManager.LocalClientId) return;
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
        private void SpawnPawnToMapClientRPC(PawnContainer pawnContainer, int containerIndex, ulong ownerClientId)
        {
            var mapPawn = CreateMapPawn(pawnContainer, containerIndex, ownerClientId);
            _containerIndexToMapPawnDictionary.Add(containerIndex, mapPawn);
        }
        
        [ServerRpc(RequireOwnership = false)]
        public void RemovePawnFromMapServerRPC(int pawnContainerIndex, ServerRpcParams serverRpcParams = default)
        {
            var clientId = serverRpcParams.Receive.SenderClientId;
            if (!NetworkManager.ConnectedClients.ContainsKey(clientId)) return;
            
            var mapPawnContainer = _mapPawnContainers[pawnContainerIndex];
            if (mapPawnContainer.ClientOwnerID != clientId) return;
            
            _mapPawnContainers[pawnContainerIndex] = EmptyPawnContainer;
            
            RemovePawnFromMapClientRPC(pawnContainerIndex);
        }
        
        [ClientRpc]
        public void RemovePawnFromMapClientRPC(int pawnContainerIndex)
        {
            var mapPawn = GetPlayerPawn(pawnContainerIndex);
            _containerIndexToMapPawnDictionary.Remove(pawnContainerIndex);
            SimulationManager.Instance.AddCoroutineSimulationObject(mapPawn.Death());
        }

        [ServerRpc(RequireOwnership = false)]
        public void StartMovePawnServerRPC(int pawnContainerIndex, int stepCount, ServerRpcParams serverRpcParams = default)
        {
            var clientId = serverRpcParams.Receive.SenderClientId;
            if (!NetworkManager.ConnectedClients.ContainsKey(clientId)) return;
            
            var mapPawnContainer = _mapPawnContainers[pawnContainerIndex];
            if (mapPawnContainer.ClientOwnerID != clientId) return;
            
            // Start Move Logic

            stepCount += mapPawnContainer.PawnStatContainer.MovementSpeed;
            
            StartMovePawnClientRPC(pawnContainerIndex, mapPawnContainer.StandingMapCell, stepCount);
            
        }
        
        
        [ClientRpc]
        private void StartMovePawnClientRPC(int pawnContainerIndex, int startMapCellIndex ,int stepCount)
        {
            var mapPawn = GetPlayerPawn(pawnContainerIndex);
            
            SimulationManager.Instance.AddCoroutineSimulationObject(mapPawn.StartMove(startMapCellIndex, stepCount));
        }
        
        

        [ServerRpc(RequireOwnership = false)]
        public void EndMovePawnServerRPC(int pawnContainerIndex, int finalMapCellIndex, ServerRpcParams serverRpcParams = default)
        {
            var clientId = serverRpcParams.Receive.SenderClientId;
            if (!NetworkManager.ConnectedClients.ContainsKey(clientId)) return;
            
            var mapPawnContainer = _mapPawnContainers[pawnContainerIndex];
            if (mapPawnContainer.ClientOwnerID != clientId) return;
            
            // End Move Logic
            
            mapPawnContainer.StandingMapCell = finalMapCellIndex;
            _mapPawnContainers[pawnContainerIndex] = mapPawnContainer;
            
            EndMovePawnClientRPC(pawnContainerIndex, finalMapCellIndex);
        }

        [ClientRpc]
        private void EndMovePawnClientRPC(int pawnContainerIndex, int finalMapCellIndex)
        {
            var mapPawn = GetPlayerPawn(pawnContainerIndex);
            
            SimulationManager.Instance.AddCoroutineSimulationObject(mapPawn.EndMove(finalMapCellIndex));
            
        }


        [ServerRpc(RequireOwnership = false)]
        public void MakeCombatServerRPC(int attackerPawnContainerIndex, int defenderPawnContainerIndex, ServerRpcParams serverRpcParams = default)
        {
            var clientId = serverRpcParams.Receive.SenderClientId;
            if (!NetworkManager.ConnectedClients.ContainsKey(clientId)) return;
            
            var attackerPawnContainer = _mapPawnContainers[attackerPawnContainerIndex];
            if (attackerPawnContainer.ClientOwnerID != clientId) return;   
            
            // Attack Logic
            var defenderPawnContainer = _mapPawnContainers[defenderPawnContainerIndex];
            
            defenderPawnContainer.PawnStatContainer.CurrentHealth -= attackerPawnContainer.PawnStatContainer.AttackDamage;
            
            MakeCombatClientRPC(attackerPawnContainerIndex, defenderPawnContainerIndex);
        }
        
        [ClientRpc]
        private void MakeCombatClientRPC(int attackerPawnContainerIndex, int defenderPawnContainerIndex)
        {
            var attackerMapPawn = GetPlayerPawn(attackerPawnContainerIndex);
            var defenderMapPawn = GetPlayerPawn(defenderPawnContainerIndex);
            
            SimulationManager.Instance.AddCoroutineSimulationObject(attackerMapPawn.Attack(defenderMapPawn));
            
            SimulationManager.Instance.AddCoroutineSimulationObject(defenderMapPawn.Defend(attackerMapPawn));
        }

    }
}