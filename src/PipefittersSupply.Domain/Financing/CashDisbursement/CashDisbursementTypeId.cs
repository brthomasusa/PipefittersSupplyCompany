using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Financing.CashDisbursement
{
    public class CashDisbursementTypeId : Value<CashDisbursementTypeId>
    {
        public int Value { get; internal set; }

        protected CashDisbursementTypeId() { }


        public CashDisbursementTypeId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("CashDisbursement type Id must be specified", nameof(value));
            }

            Value = value;
        }

        public static implicit operator int(CashDisbursementTypeId self) => self.Value;

        public static implicit operator CashDisbursementTypeId(string value) => new CashDisbursementTypeId(int.Parse(value));

        public bool Equals(CashDisbursementTypeId other)
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

            return Equals((CashDisbursementTypeId)obj);
        }

        public override int GetHashCode() => Value.GetHashCode();

        public override string ToString() => Value.ToString();
    }
}