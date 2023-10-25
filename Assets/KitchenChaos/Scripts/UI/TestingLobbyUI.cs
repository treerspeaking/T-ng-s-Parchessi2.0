using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers.Network;
using UnityEngine;
using UnityEngine.UI;

public class TestingLobbyUI : MonoBehaviour {


    [SerializeField] private Button createGameButton;
    [SerializeField] private Button joinGameButton;


    private void Awake() {
        createGameButton.onClick.AddListener(() => {
            KitchenGameMultiplayer.Instance.StartHost();
            AssetNetworkSceneManager.LoadNetworkScene(AssetSceneManager.AssetScene.CharacterSelectScene.ToString());
        });
        joinGameButton.onClick.AddListener(() => {
            KitchenGameMultiplayer.Instance.StartClient();
        });
    }

}