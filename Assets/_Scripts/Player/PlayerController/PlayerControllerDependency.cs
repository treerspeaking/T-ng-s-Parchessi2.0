using Unity.Netcode;

public class PlayerControllerDependency : NetworkBehaviour
{
    protected PlayerController PlayerController;

    public void Initialize(PlayerController playerController)
    {
        PlayerController = playerController;
    }
}
