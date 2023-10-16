using System;

namespace _Scripts.UI.GameUI
{
    public class EndRollButtonController : ButtonControllerDependency
    {
        private void Start()
        {
            Button.onClick.AddListener(GameManager.Instance.ClientOwnerPlayerController.PlayerTurnController.EndRollPhaseServerRPC);
        }
    }
}