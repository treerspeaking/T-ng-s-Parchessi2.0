using System;
using Shun_Card_System;
using UnityEngine;

namespace _Scripts.Managers.Game
{
    public class InputManager : MonoBehaviour
    {
        private BaseDraggableObjectMouseInput _draggableObjectMouseInput = new BaseDraggableObjectMouseInput();

        private void Update()
        {
            _draggableObjectMouseInput.UpdateMouseInput();
        }
    }
}