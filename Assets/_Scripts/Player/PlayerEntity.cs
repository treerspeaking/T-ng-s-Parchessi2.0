using UnityEngine;

public class PlayerEntity : MonoBehaviour
{
    public int ContainerIndex { get; protected set; }

    public ulong OwnerClientID { get; protected set; }
    
    public void Initialize(int containerIndex, ulong ownerClientID)
    {
        ContainerIndex = containerIndex;
        OwnerClientID = ownerClientID;
    }
}