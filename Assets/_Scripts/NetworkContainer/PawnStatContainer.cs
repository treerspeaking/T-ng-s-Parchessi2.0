using System;
using Unity.Netcode;

namespace _Scripts.NetworkContainter
{
    public struct PawnStatContainer : INetworkSerializable, IEquatable<PawnStatContainer>
    {
        public int PawnStatID;
        public int AttackDamage;
        public int MaxHealth;
        public int CurrentHealth;
        public int MovementSpeed;


        public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
        {
            serializer.SerializeValue(ref AttackDamage);
            serializer.SerializeValue(ref MaxHealth);
            serializer.SerializeValue(ref CurrentHealth);
            serializer.SerializeValue(ref MovementSpeed);
        }

        public bool Equals(PawnStatContainer other)
        {
            return AttackDamage == other.AttackDamage &&
                   MaxHealth == other.MaxHealth &&
                   CurrentHealth == other.CurrentHealth &&
                   MovementSpeed == other.MovementSpeed;
        }

    }
}