using _Scripts.NetworkContainter;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerActionController : PlayerControllerDependency
{
    NetworkList <ActionContainer> _targetContainers;

    public override void Awake()
    {
        base.Awake();
        _targetContainers = new();
    }

    [ServerRpc]
    public void PlayTargetServerRPC(ActionContainer actionContainer)
    {
        _targetContainers.Add(actionContainer);
        PlayTargetClientRPC(actionContainer);
    }
    
    [ClientRpc]
    private void PlayTargetClientRPC(ActionContainer actionContainer)
    {
        Debug.Log("Client Play Action RPC");
    }
}
