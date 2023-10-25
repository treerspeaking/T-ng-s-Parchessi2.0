using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class LobbyMessageUI : MonoBehaviour {


    [FormerlySerializedAs("messageText")] [SerializeField] private TextMeshProUGUI _messageText;
    [FormerlySerializedAs("closeButton")] [SerializeField] private Button _closeButton;


    private void Awake() {
        _closeButton.onClick.AddListener(Hide);
    }

    private void Start() {
        GameMultiplayerManager.Instance.OnFailedToJoinGame += KitchenGameMultiplayer_OnFailedToJoinGame;
        GameLobbyManager.Instance.OnCreateLobbyStarted += KitchenGameLobby_OnCreateLobbyStarted;
        GameLobbyManager.Instance.OnCreateLobbyFailed += KitchenGameLobby_OnCreateLobbyFailed;
        GameLobbyManager.Instance.OnJoinStarted += KitchenGameLobby_OnJoinStarted;
        GameLobbyManager.Instance.OnJoinFailed += KitchenGameLobby_OnJoinFailed;
        GameLobbyManager.Instance.OnQuickJoinFailed += KitchenGameLobby_OnQuickJoinFailed;

        Hide();
    }

    private void KitchenGameLobby_OnQuickJoinFailed(object sender, System.EventArgs e) {
        ShowMessage("Could not find a Lobby to Quick Join!");
    }

    private void KitchenGameLobby_OnJoinFailed(object sender, System.EventArgs e) {
        ShowMessage("Failed to join Lobby!");
    }

    private void KitchenGameLobby_OnJoinStarted(object sender, System.EventArgs e) {
        ShowMessage("Joining Lobby...");
    }

    private void KitchenGameLobby_OnCreateLobbyFailed(object sender, System.EventArgs e) {
        ShowMessage("Failed to create Lobby!");
    }

    private void KitchenGameLobby_OnCreateLobbyStarted(object sender, System.EventArgs e) {
        ShowMessage("Creating Lobby...");
    }

    private void KitchenGameMultiplayer_OnFailedToJoinGame(object sender, System.EventArgs e) {
        if (NetworkManager.Singleton.DisconnectReason == "") {
            ShowMessage("Failed to connect");
        } else {
            ShowMessage(NetworkManager.Singleton.DisconnectReason);
        }
    }

    private void ShowMessage(string message) {
        Show();
        _messageText.text = message;
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        if(GameMultiplayerManager.Instance != null) GameMultiplayerManager.Instance.OnFailedToJoinGame -= KitchenGameMultiplayer_OnFailedToJoinGame;
        GameLobbyManager.Instance.OnCreateLobbyStarted -= KitchenGameLobby_OnCreateLobbyStarted;
        GameLobbyManager.Instance.OnCreateLobbyFailed -= KitchenGameLobby_OnCreateLobbyFailed;
        GameLobbyManager.Instance.OnJoinStarted -= KitchenGameLobby_OnJoinStarted;
        GameLobbyManager.Instance.OnJoinFailed -= KitchenGameLobby_OnJoinFailed;
        GameLobbyManager.Instance.OnQuickJoinFailed -= KitchenGameLobby_OnQuickJoinFailed;
    }

}