using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtonController : ButtonControllerDependency
{
    private void Start()
    {
        Button.onClick.AddListener(GameManager.Instance.ClientOwnerPlayerController.PlayerTurnController.EndTurnServerRPC);
    }
}
