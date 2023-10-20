
using System;
using _Scripts.NetworkContainter;
using Unity.Netcode;

public struct PawnContainer: INetworkSerializable, IEquatable<PawnContainer>
{
    public int PawnID;
    
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref PawnID);
    }

    public bool Equals(PawnContainer other)
    {
        return PawnID == other.PawnID;
    }
        
        
}