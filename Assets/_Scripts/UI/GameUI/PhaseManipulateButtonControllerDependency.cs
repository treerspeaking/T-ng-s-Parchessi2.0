
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Button))]
public abstract class PhaseManipulateButtonControllerDependency : MonoBehaviour
{
    protected Button Button;
    protected PlayerController PlayerController;
    
    private void Awake()
    {
        Button = GetComponent<Button>();
        GameManager.Instance.OnGameSetUp += () =>
        {
            PlayerController = GameManager.Instance.ClientOwnerPlayerController;
            GameSetUp();
        };
    }

    protected abstract void GameSetUp();

}
