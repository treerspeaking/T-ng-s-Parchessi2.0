using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public PlayerTurnController PlayerTurnController { get; internal set; }
    public PlayerActionController PlayerActionController { get; internal set; }
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
        PlayerTurnController = GetComponent<PlayerTurnController>();
        PlayerActionController = GetComponent<PlayerActionController>();
        PlayerResourceController = GetComponent<PlayerResourceController>();
        GameManager.Instance.StartPlayerController(this);
    }

    
    
}