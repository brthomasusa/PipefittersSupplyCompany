using System;

namespace PipefittersSupply.Domain.Purchasing.InventoryReceipt
{
    public class InventoryReceiptDetailId : IEquatable<InventoryReceiptDetailId>
    {
        private int Value { get; }

        public InventoryReceiptDetailId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Inventory receipt detail Id must be specified", nameof(value));
            }

            Value = value;
        }

        public static implicit operator int(InventoryReceiptDetailId self) => self.Value;

        public static implicit operator InventoryReceiptDetailId(string value) => new InventoryReceiptDetailId(int.Parse(value));

        public bool Equals(InventoryReceiptDetailId other)
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

            return Value.Equals((InventoryReceiptDetailId)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}