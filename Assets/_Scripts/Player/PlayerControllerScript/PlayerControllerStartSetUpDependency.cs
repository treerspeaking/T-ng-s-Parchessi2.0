using UnityEngine;

public abstract class PlayerControllerStartSetUpDependency : MonoBehaviour
{
    protected PlayerController PlayerController;

    protected virtual void Awake()
    {
        GameManager.Instance.OnGameSetUp += () =>
        {
            PlayerController = GameManager.Instance.ClientOwnerPlayerController;
            GameSetUp();
        };
    }

    protected virtual void GameSetUp()
    {
        
    }
}
