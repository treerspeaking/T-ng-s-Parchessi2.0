using System.Collections;
using System.Collections.Generic;
using _Scripts.Managers.Network;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TestingLobbyUI : MonoBehaviour {


    [FormerlySerializedAs("createGameButton")] [SerializeField] private Button _createGameButton;
    [FormerlySerializedAs("joinGameButton")] [SerializeField] private Button _joinGameButton;


    private void Awake() {
        _createGameButton.onClick.AddListener(() => {
            GameMultiplayerManager.Instance.StartHost();
            AssetNetworkSceneManager.LoadNetworkScene(AssetSceneManager.AssetScene.CharacterSelectScene.ToString());
        });
        _joinGameButton.onClick.AddListener(() => {
            GameMultiplayerManager.Instance.StartClient();
        });
    }

}