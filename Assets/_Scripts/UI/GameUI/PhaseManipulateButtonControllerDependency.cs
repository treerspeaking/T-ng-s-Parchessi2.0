using _Scripts.Player.PlayerControllerScript;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public abstract class PhaseManipulateButtonControllerDependency : PlayerControllerStartSetUpDependency
{
    protected Button Button;
    
    protected override void Awake()
    {
        base.Awake();
        Button = GetComponent<Button>();
    }

}
