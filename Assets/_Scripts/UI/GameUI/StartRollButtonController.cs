using System;
using UnityEngine;
using UnityEngine.Rendering;


public class StartRollButtonController : ButtonControllerDependency
{
    private void Start()
    {
        Button.onClick.AddListener(GameManager.Instance.ClientOwnerPlayerController.PlayerTurnController.StartRollPhaseServerRPC);
    }
}
