using System;
using _Scripts.UI.GameUI;
using UnityEngine;
using UnityEngine.UI;

public class PhaseManipulateButtonAccumulation : MonoBehaviour
{
    PlayerController _playerController;
    
    [SerializeField] private Button _waitButton;
    [SerializeField] private EndTurnButtonController _endTurnButtonController;
    [SerializeField] private EndRollButtonController _endRollButtonController;
    [SerializeField] private StartRollButtonController _startRollButtonController;
    
    private GameObject _currentActiveButton;

    private void Start()
    {
        _playerController = GameManager.Instance.ClientOwnerPlayerController;
        _playerController.PlayerTurnController.CurrentPlayerPhase.OnValueChanged += OnCurrentPlayerPhaseChanged;
    }

    private void OnCurrentPlayerPhaseChanged(PlayerTurnController.PlayerPhase previousValue, PlayerTurnController.PlayerPhase newValue)
    {
        switch (newValue)
        {
            case PlayerTurnController.PlayerPhase.Wait:
                SwapButton(_waitButton.gameObject);
                break;
            case PlayerTurnController.PlayerPhase.Preparation:
                SwapButton(_startRollButtonController.gameObject);
                break;
            case PlayerTurnController.PlayerPhase.Roll:
                SwapButton(_endRollButtonController.gameObject);
                break;
            case PlayerTurnController.PlayerPhase.Subsequence:
                SwapButton(_endTurnButtonController.gameObject);
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(newValue), newValue, null);
        }
    }

    private void SwapButton(GameObject newButton)
    {
        if (_currentActiveButton == null) return;

        _currentActiveButton.SetActive(false);
        _currentActiveButton = newButton;
        _currentActiveButton.SetActive(true);
    }
    
    
    
    
}
