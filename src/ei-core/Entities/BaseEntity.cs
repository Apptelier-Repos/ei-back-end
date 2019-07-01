using System;
using Ardalis.GuardClauses;
using ei_core.Interfaces;

namespace ei_core.Entities
{
    public abstract class BaseEntity : IIdentifiable, IEquatable<BaseEntity>
    {
        public int Id { get; }

        internal BaseEntity(int id)
        {
            Guard.Against.Zero(id, nameof(id));
            Id = id;
        }

        public bool Equals(BaseEntity other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((BaseEntity) obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}