using System;
using Unity.Netcode;

namespace _Scripts.NetworkContainter
{
    public struct DiceContainer : INetworkSerializable, IEquatable<DiceContainer>
    {
        public int DiceID;
        
        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref DiceID);
        }

        public bool Equals(DiceContainer other)
        {
            return DiceID == other.DiceID;
        }
    }
}