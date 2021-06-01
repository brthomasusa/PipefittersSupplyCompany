using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Financing.CashDisbursement
{
    public class PayeeTypeName : Value<PayeeTypeName>
    {
        public string Value { get; }

        protected PayeeTypeName() { }

        internal PayeeTypeName(string value) => Value = value;

        public static implicit operator string(PayeeTypeName self) => self.Value;

        public static PayeeTypeName FromString(string value)
        {
            CheckValidity(value);
            return new PayeeTypeName(value);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The payee type name is required.", nameof(value));
            }

            if (value.Length > 25)
            {
                throw new ArgumentOutOfRangeException("The payee type name can not be longer than 25 characters.", nameof(value));
            }
        }
    }
}