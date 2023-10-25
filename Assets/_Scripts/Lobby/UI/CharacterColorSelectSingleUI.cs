using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class CharacterColorSelectSingleUI : MonoBehaviour 
{
    
    [FormerlySerializedAs("colorId")] [SerializeField] private int _colorId;
    [FormerlySerializedAs("image")] [SerializeField] private Image _image;
    [FormerlySerializedAs("selectedGameObject")] [SerializeField] private GameObject _selectedGameObject;

    private void Awake() {
        GetComponent<Button>().onClick.AddListener(() => {
            GameMultiplayerManager.Instance.ChangePlayerColor(_colorId);
        });
    }

    private void Start() {
        GameMultiplayerManager.Instance.OnPlayerContainerNetworkListChanged += KitchenGameMultiplayer_OnPlayerDataNetworkListChanged;
        _image.color = GameMultiplayerManager.Instance.GetPlayerColor(_colorId);
        UpdateIsSelected();
    }

    private void KitchenGameMultiplayer_OnPlayerDataNetworkListChanged(object sender, System.EventArgs e) {
        UpdateIsSelected();
    }

    private void UpdateIsSelected() {
        if (GameMultiplayerManager.Instance.GetPlayerContainer().ColorID == _colorId) {
            _selectedGameObject.SetActive(true);
        } else {
            _selectedGameObject.SetActive(false);
        }
    }

    private void OnDestroy() {
        if (GameMultiplayerManager.Instance != null) GameMultiplayerManager.Instance.OnPlayerContainerNetworkListChanged -= KitchenGameMultiplayer_OnPlayerDataNetworkListChanged;
    }
}