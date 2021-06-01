using System;

namespace PipefittersSupply.Domain.Purchasing.InventoryReceipt
{
    public class InventoryReceiptId : IEquatable<InventoryReceiptId>
    {
        private int Value { get; }

        protected InventoryReceiptId() { }

        public InventoryReceiptId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Inventory receipt Id must be specified", nameof(value));
            }

            Value = value;
        }

        public static implicit operator int(InventoryReceiptId self) => self.Value;

        public static implicit operator InventoryReceiptId(string value) => new InventoryReceiptId(int.Parse(value));

        public bool Equals(InventoryReceiptId other)
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

            return Value.Equals((InventoryReceiptId)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}