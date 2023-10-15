using System;
using Unity.Netcode;

namespace _Scripts.NetworkContainter
{
    public struct CardContainer  : INetworkSerializable, IEquatable<PlayerContainer>
    {
        public int ID;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref ID);
        }

        public bool Equals(PlayerContainer other)
        {
            return ID == other.ID;
        }
    }
}