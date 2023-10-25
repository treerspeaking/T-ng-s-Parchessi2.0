
using System;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public abstract class PhaseManipulateButtonControllerDependency : PlayerControllerCompositionDependency
{
    protected Button Button;

    protected void Awake()
    {
        
        Button = GetComponent<Button>();
    }

    protected void Start()
    {
        GameManager.Instance.OnGameStart += GameSetUp;
    }

    protected abstract void GameSetUp();
}
