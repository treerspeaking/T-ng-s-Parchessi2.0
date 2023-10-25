using System.Collections;
using System.Collections.Generic;
using _Scripts.NetworkContainter;
using TMPro;
using Unity.Netcode;
using UnityEngine;
using UnityEngine.UI;

public class CharacterSelectPlayer : MonoBehaviour {


    [SerializeField] private int playerIndex;
    [SerializeField] private GameObject readyGameObject;
    
    [SerializeField] private Button kickButton;
    [SerializeField] private TextMeshPro playerNameText;


    private void Awake() {
        kickButton.onClick.AddListener(() => {
            PlayerContainer playerData = KitchenGameMultiplayer.Instance.GetPlayerContainerFromPlayerIndex(playerIndex);
            KitchenGameLobby.Instance.KickPlayer(playerData.PlayerID.ToString());
            KitchenGameMultiplayer.Instance.KickPlayer(playerData.ClientID);
        });
    }

    private void Start() {
        KitchenGameMultiplayer.Instance.OnPlayerContainerNetworkListChanged += KitchenGameMultiplayer_OnPlayerContainerNetworkListChanged;
        CharacterSelectReady.Instance.OnReadyChanged += CharacterSelectReady_OnReadyChanged;

        kickButton.gameObject.SetActive(NetworkManager.Singleton.IsServer);

        UpdatePlayer();
    }

    private void CharacterSelectReady_OnReadyChanged(object sender, System.EventArgs e) {
        UpdatePlayer();
    }

    private void KitchenGameMultiplayer_OnPlayerContainerNetworkListChanged(object sender, System.EventArgs e) {
        UpdatePlayer();
    }

    private void UpdatePlayer() {
        if (KitchenGameMultiplayer.Instance.IsPlayerIndexConnected(playerIndex)) {
            Show();

            PlayerContainer playerData = KitchenGameMultiplayer.Instance.GetPlayerContainerFromPlayerIndex(playerIndex);

            readyGameObject.SetActive(CharacterSelectReady.Instance.IsPlayerReady(playerData.ClientID));

            playerNameText.text = playerData.PlayerName.ToString();

            
        } else {
            Hide();
        }
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        KitchenGameMultiplayer.Instance.OnPlayerContainerNetworkListChanged -= KitchenGameMultiplayer_OnPlayerContainerNetworkListChanged;
    }


}