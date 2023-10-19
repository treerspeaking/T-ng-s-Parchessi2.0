
using System;
using _Scripts.NetworkContainter;
using Unity.Netcode;

public class PawnContainer: INetworkSerializable, IEquatable<PawnContainer>
{
    public int PawnID;
    public int CurrentStayingMapCellIndex;
    public int CurrentStayingMapSpotIndex;
    
    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref PawnID);
        serializer.SerializeValue(ref CurrentStayingMapSpotIndex);
        serializer.SerializeValue(ref CurrentStayingMapCellIndex);
    }

    public bool Equals(PawnContainer other)
    {
        return PawnID == other.PawnID;
    }
        
        
}