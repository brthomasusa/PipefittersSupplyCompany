using System;
using PipefittersSupply.Domain.Base;
namespace PipefittersSupply.Domain.Purchasing.PurchaseOrder
{
    public class PurchaseOrderDetailId : Value<PurchaseOrderDetailId>
    {
        private int Value { get; }

        protected PurchaseOrderDetailId() { }

        public PurchaseOrderDetailId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Purchase order detail Id must be specified", nameof(value));
            }

            Value = value;
        }

        public static implicit operator int(PurchaseOrderDetailId self) => self.Value;

        public static implicit operator PurchaseOrderDetailId(string value) => new PurchaseOrderDetailId(int.Parse(value));

        public bool Equals(PurchaseOrderDetailId other)
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

            return Value.Equals((PurchaseOrderDetailId)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}