using System;
using System.Collections;
using System.Collections.Generic;
using _Scripts.Player;
using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public PlayerTurnController PlayerTurnController { get; private set; }
    public PlayerActionController PlayerActionController { get; private set; }
    private PlayerHand _playerHand;

    private void Start()
    {
        GameManager.Instance.AddPlayerController(this);
        SpawnTurnController();
        SpawnActionController();
    }

    void SpawnTurnController()
    {
        if (!IsServer) return;
        
        PlayerTurnController = Instantiate(GameResourceManager.Instance.PlayerTurnControllerPrefab);
        PlayerTurnController.NetworkObject.SpawnWithOwnership(OwnerClientId);
        PlayerTurnController.transform.parent = transform;
        PlayerTurnController.Initialize(this);

    }

    
    void SpawnActionController()
    {
        if (!IsServer) return;
        
        PlayerActionController = Instantiate(GameResourceManager.Instance.PlayerActionControllerPrefab);
        PlayerActionController.NetworkObject.SpawnWithOwnership(OwnerClientId);
        PlayerActionController.transform.parent = transform;
        PlayerActionController.Initialize(this);
        
    }
    
    public override void OnNetworkDespawn()
    {
        base.OnNetworkDespawn();
    }
}
