using System;
using Unity.Netcode;

namespace _Scripts.NetworkContainter
{
    public struct CardContainer : INetworkSerializable, IEquatable<CardContainer>
    {
        public int CardID;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref CardID);
        }

        public bool Equals(CardContainer other)
        {
            return CardID == other.CardID;
        }
    }
}