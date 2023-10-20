using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public PlayerTurnControllerRequire PlayerTurnControllerRequire { get; internal set; }
    public PlayerActionControllerRequire PlayerActionControllerRequire { get; internal set; }
    public PlayerResourceController PlayerResourceController { get; internal set; }

    public ClientRpcParams MyClientRpcParams;

    private void Awake()
    {
        MyClientRpcParams  = new ClientRpcParams
        {
            Send = new ClientRpcSendParams
            {
                TargetClientIds = new ulong[]{OwnerClientId}
            }
        };
    }

    private void Start()
    {
        PlayerTurnControllerRequire = GetComponent<PlayerTurnControllerRequire>();
        PlayerActionControllerRequire = GetComponent<PlayerActionControllerRequire>();
        PlayerResourceController = GetComponent<PlayerResourceController>();
        GameManager.Instance.StartPlayerController(this);
    }

    
    
}