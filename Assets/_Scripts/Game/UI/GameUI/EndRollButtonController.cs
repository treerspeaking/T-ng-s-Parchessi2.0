using System;

namespace _Scripts.UI.GameUI
{
    public class EndRollButtonController : PhaseManipulateButtonControllerDependency
    {
        protected override void GameSetUp()
        {
            Button.onClick.AddListener(GameManager.Instance.ClientOwnerPlayerController.PlayerTurnController.EndRollPhaseServerRPC);
        }
    }
}