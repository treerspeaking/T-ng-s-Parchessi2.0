using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Services.Lobbies.Models;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LobbyListSingleUI : MonoBehaviour 
{
    [FormerlySerializedAs("lobbyNameText")] [SerializeField] private TextMeshProUGUI _lobbyNameText;
    
    private Lobby _lobby;


    private void Awake() {
        GetComponent<Button>().onClick.AddListener(() => {
            GameLobbyManager.Instance.JoinWithId(_lobby.Id);
        });
    }

    public void SetLobby(Lobby lobby) {
        this._lobby = lobby;
        _lobbyNameText.text = lobby.Name;
    }

}