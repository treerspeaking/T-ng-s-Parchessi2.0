using UnityEngine;

public abstract class PlayerControllerCompositionDependency : MonoBehaviour
{
    protected PlayerController PlayerController;
    
    public void Initialize(PlayerController playerController)
    {
        PlayerController = playerController;
        
    }
}
