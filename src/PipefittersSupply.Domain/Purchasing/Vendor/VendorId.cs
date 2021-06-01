using System;

namespace PipefittersSupply.Domain.Purchasing.Vendor
{
    public class VendorId : IEquatable<VendorId>
    {
        private int Value { get; }

        protected VendorId() { }

        public VendorId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Vendor Id must be specified", nameof(value));
            }

            Value = value;
        }

        public static implicit operator int(VendorId self) => self.Value;

        public static implicit operator VendorId(string value) => new VendorId(int.Parse(value));

        public bool Equals(VendorId other)
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

            return Value.Equals((VendorId)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}