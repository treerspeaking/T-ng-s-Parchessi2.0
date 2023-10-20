using System;
using Unity.Netcode;

namespace _Scripts.NetworkContainter
{
    public struct ActionContainer : INetworkSerializable, IEquatable<ActionContainer>
    {
        public TargetContainer TargeterContainer;
        public TargetContainer TargeteeContainer;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref TargeterContainer);
            serializer.SerializeValue(ref TargeteeContainer);
        }

        public bool Equals(ActionContainer other)
        {
            return TargeterContainer.Equals(other.TargeterContainer) &&
                   TargeteeContainer.Equals(other.TargeteeContainer);
        }
    }
}