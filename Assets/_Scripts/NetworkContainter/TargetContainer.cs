using System;
using Unity.Netcode;

namespace _Scripts.NetworkContainter
{
    public struct TargetContainer : INetworkSerializable, IEquatable<TargetContainer>
    {
        public TargetType TargeterType;
        public int TargeterContainerIndex;
        public TargetType TargeteeType;
        public int TargeteeContainerIndex;
        
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref TargeterType);
            serializer.SerializeValue(ref TargeterContainerIndex);
            serializer.SerializeValue(ref TargeteeType);
            serializer.SerializeValue(ref TargeteeContainerIndex);
        }

        public bool Equals(TargetContainer other)
        {
            return TargeterType == other.TargeterType 
                   && TargeterContainerIndex == other.TargeterContainerIndex 
                   && TargeteeType == other.TargeteeType 
                   && TargeteeContainerIndex == other.TargeteeContainerIndex;
        }
    }
}