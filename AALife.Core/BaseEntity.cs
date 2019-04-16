using System;
using System.ComponentModel.DataAnnotations;

namespace AALife.Core
{
    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntity<TPrimaryKey>
    {
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [Key]
        public TPrimaryKey Id { get; set; }

        #region method

        public override bool Equals(object obj)
        {
            return Equals(obj as BaseEntity<TPrimaryKey>);
        }

        private static bool IsTransient(BaseEntity<TPrimaryKey> obj)
        {
            return obj != null && Equals(obj.Id, default(TPrimaryKey));
        }

        private Type GetUnproxiedType()
        {
            return GetType();
        }

        public virtual bool Equals(BaseEntity<TPrimaryKey> other)
        {
            if (other == null)
                return false;

            if (ReferenceEquals(this, other))
                return true;

            if (!IsTransient(this) &&
                !IsTransient(other) &&
                Equals(Id, other.Id))
            {
                var otherType = other.GetUnproxiedType();
                var thisType = GetUnproxiedType();
                return thisType.IsAssignableFrom(otherType) ||
                        otherType.IsAssignableFrom(thisType);
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Equals(Id, default(TPrimaryKey)))
                return base.GetHashCode();
            return Id.GetHashCode();
        }

        public static bool operator ==(BaseEntity<TPrimaryKey> x, BaseEntity<TPrimaryKey> y)
        {
            return Equals(x, y);
        }

        public static bool operator !=(BaseEntity<TPrimaryKey> x, BaseEntity<TPrimaryKey> y)
        {
            return !(x == y);
        }

        #endregion
    }

    /// <summary>
    /// Base class for entities
    /// </summary>
    public abstract partial class BaseEntity : BaseEntity<int>
    {
    }
}