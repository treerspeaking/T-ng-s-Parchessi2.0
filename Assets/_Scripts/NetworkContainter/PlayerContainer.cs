using System;
using Unity.Netcode;

namespace _Scripts.NetworkContainter
{
    public struct PlayerContainer : INetworkSerializable, IEquatable<PlayerContainer>
    {
        public int PlayerID;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref PlayerID);
            
        }

        public bool Equals(PlayerContainer other)
        {
            return PlayerID == other.PlayerID;
        }
        
        
    }
}