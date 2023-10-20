using System;
using Unity.Netcode;

namespace _Scripts.NetworkContainter
{
    public struct TargetContainer : INetworkSerializable, IEquatable<TargetContainer>
    {
        public TargetType TargetType;
        public int TargetContainerIndex;
        public ulong TargetClientOwnerId;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref TargetType);
            serializer.SerializeValue(ref TargetContainerIndex);
            serializer.SerializeValue(ref TargetClientOwnerId);
        }

        public bool Equals(TargetContainer other)
        {
            return TargetType == other.TargetType 
                   && TargetContainerIndex == other.TargetContainerIndex 
                     && TargetClientOwnerId == other.TargetClientOwnerId;
        }
        
        
    }
}