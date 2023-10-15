using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;
using UnityUtilities;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField] private int _activePlayer = 4;

    private readonly List<PlayerController> _playerControllers = new ();

    public void AddPlayerController(PlayerController playerController)
    {
        _playerControllers.Add(playerController);
    }
}