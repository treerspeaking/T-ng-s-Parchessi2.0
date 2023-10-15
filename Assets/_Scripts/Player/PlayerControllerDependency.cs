using Unity.Netcode;

namespace _Scripts.Player
{
    public class PlayerControllerDependency : NetworkBehaviour
    {
        protected PlayerController PlayerController;

        public void Initialize(PlayerController playerController)
        {
            PlayerController = playerController;
        }
    }
}