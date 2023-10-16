using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public PlayerTurnController PlayerTurnController { get; internal set; }
    public PlayerActionController PlayerActionController { get; internal set; }
    private PlayerCardHand _playerCardHand;

    private void Start()
    {
        PlayerTurnController = GetComponent<PlayerTurnController>();
        PlayerActionController = GetComponent<PlayerActionController>();
        GameManager.Instance.StartPlayerController(this);
    }

    
    
}
