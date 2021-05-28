using System;
using PipefittersSupply.Framework;
namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class PurchaseOrderId : Value<PurchaseOrderId>
    {
        private int Value { get; }

        public PurchaseOrderId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Purchase order Id must be specified", nameof(value));
            }

            Value = value;
        }

        public static implicit operator int(PurchaseOrderId self) => self.Value;

        public static implicit operator PurchaseOrderId(string value) => new PurchaseOrderId(int.Parse(value));

        public bool Equals(PurchaseOrderId other)
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

            return Value.Equals((PurchaseOrderId)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}