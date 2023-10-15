using System;
using System.Collections;
using System.Collections.Generic;

using Unity.Netcode;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    public PlayerTurnController PlayerTurnController { get; internal set; }
    public PlayerActionController PlayerActionController { get; internal set; }
    private PlayerHand _playerHand;

    private void Start()
    {
        GameManager.Instance.AddPlayerController(this);
        PlayerTurnController = GetComponent<PlayerTurnController>();
        PlayerActionController = GetComponent<PlayerActionController>();
    }

    
    
}
