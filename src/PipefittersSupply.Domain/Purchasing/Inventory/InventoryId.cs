using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Purchasing.Inventory
{
    public class InventoryId : IEquatable<InventoryId>
    {
        private int Value { get; }

        public InventoryId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Inventory Id must be specified", nameof(value));
            }

            Value = value;
        }

        public static implicit operator int(InventoryId self) => self.Value;

        public static implicit operator InventoryId(string value) => new InventoryId(int.Parse(value));

        public bool Equals(InventoryId other)
        {
            if (ReferenceEquals(null, other))
            {
                return false;
            }

            if (ReferenceEquals(this, other))
            {
                return true;
            }

            return Value.Equals(other.Value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != this.GetType())
            {
                return false;
            }

            return Value.Equals((InventoryId)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}