using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int _activePlayer = 4;

    private List<PlayerTurnController> _playerTurnControllers;

    /*
    void Start()
    {
        for (int i = 0; i < _activePlayer; i++)
        {
            _playerTurnControllers.Add(Instantiate(GameResourceManager.Instance.PlayerTurnController));
        }

        int k = 0;
        foreach (var playerTurnController in _playerTurnControllers)
        {
            Debug.Log(k+": "+ playerTurnController.CurrentPlayerPhase );
            k++;
        }
    }

    void Update()
    {
        
    }
    */
}