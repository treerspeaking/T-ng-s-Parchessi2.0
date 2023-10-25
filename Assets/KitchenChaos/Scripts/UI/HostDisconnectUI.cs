using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers.Network;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class HostDisconnectUI : MonoBehaviour {


    [SerializeField] private Button playAgainButton;


    private void Awake() {
        playAgainButton.onClick.AddListener(() => {
            AssetNetworkSceneManager.LoadNetworkScene(AssetSceneManager.AssetScene.MainMenuScene.ToString());
        });
    }

    private void Start() {
        NetworkManager.Singleton.OnClientDisconnectCallback += NetworkManager_OnClientDisconnectCallback;

        Hide();
    }

    private void NetworkManager_OnClientDisconnectCallback(ulong clientId) {
        if (clientId == NetworkManager.ServerClientId) {
            // Server is shutting down
            Show();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

}