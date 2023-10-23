using System;
using _Scripts.Player.Card;
using Unity.Netcode;

namespace _Scripts.NetworkContainter
{
    public struct CardContainer : INetworkSerializable, IEquatable<CardContainer>
    {

        public int CardID;
        public CardType CardType;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref CardID);
            serializer.SerializeValue(ref CardType);
        }

        public bool Equals(CardContainer other)
        {
            return CardID == other.CardID
                && CardType == other.CardType;
        }
    }
}