using Unity.Netcode;
using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerControllerRequireDependency : NetworkBehaviour
{
    protected PlayerController PlayerController;
    protected PlayerActionController PlayerActionController;
    protected PlayerTurnController PlayerTurnController;
    protected PlayerResourceController PlayerResourceController;
    
    public virtual void Awake()
    {
        PlayerController = GetComponent<PlayerController>();
        PlayerActionController = GetComponent<PlayerActionController>();
        PlayerTurnController = GetComponent<PlayerTurnController>();
        PlayerResourceController = GetComponent<PlayerResourceController>();
    }
}
