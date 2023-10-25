using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class TestingCharacterSelectUI : MonoBehaviour {


    [FormerlySerializedAs("readyButton")] [SerializeField] private Button _readyButton;


    private void Awake() {
        _readyButton.onClick.AddListener(() => {
            CharacterSelectReadyManager.Instance.SetPlayerReady();
        });
    }

}