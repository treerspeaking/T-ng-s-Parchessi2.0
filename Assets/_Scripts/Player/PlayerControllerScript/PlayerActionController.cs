using _Scripts.NetworkContainter;
using Unity.Collections;
using Unity.Netcode;
using UnityEngine;

public class PlayerActionController : PlayerControllerDependency
{
    NetworkList <TargetContainer> _targetContainers;

    public override void Awake()
    {
        base.Awake();
        _targetContainers = new();
    }

    [ServerRpc]
    public void PlayTargetServerRPC(TargetContainer targetContainer)
    {
        _targetContainers.Add(targetContainer);
        PlayTargetClientRPC(targetContainer);
    }
    
    [ClientRpc]
    private void PlayTargetClientRPC(TargetContainer targetContainer)
    {
        Debug.Log("Client Play Target RPC");
    }
}
