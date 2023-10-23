using System;
using Shun_Card_System;
using UnityEngine;

namespace _Scripts.Managers.Game
{
    public class InputManager : MonoBehaviour
    {
        private HandDraggableObjectMouseInput _draggableObjectMouseInput = new ();
        private bool _isHandInteractable = false;
        
        private void Awake()
        {
            GameManager.Instance.OnPlayerTurnStart += OnPlayerTurnStart;
        }

        private void OnPlayerTurnStart(PlayerController playerController)
        {
            _isHandInteractable = playerController == GameManager.Instance.ClientOwnerPlayerController;
        }

        private void Update()
        {
            if (_isHandInteractable)
                _draggableObjectMouseInput.UpdateMouseInput();
        }
    }
}