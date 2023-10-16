using UnityEngine;

public abstract class PlayerControllerStartSetUpDependency : MonoBehaviour
{
    protected global::PlayerController PlayerController;

    protected virtual void Awake()
    {
        GameManager.Instance.OnGameSetUp += () =>
        {
            PlayerController = GameManager.Instance.ClientOwnerPlayerController;
            GameSetUp();
        };
    }

    protected abstract void GameSetUp();
}
