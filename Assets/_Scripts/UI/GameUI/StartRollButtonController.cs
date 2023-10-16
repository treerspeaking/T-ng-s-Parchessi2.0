using System;
using UnityEngine;
using UnityEngine.Rendering;


public class StartRollButtonController : PhaseManipulateButtonControllerDependency
{
    protected override void GameSetUp()
    {
        Button.onClick.AddListener(GameManager.Instance.ClientOwnerPlayerController.PlayerTurnController.StartRollPhaseServerRPC);
    }
}
