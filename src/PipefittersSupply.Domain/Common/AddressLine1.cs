using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Common
{
    public class AddressLine1 : Value<AddressLine1>
    {
        public string Value { get; }

        protected AddressLine1() { }


        internal AddressLine1(string value) => Value = value;

        public static implicit operator string(AddressLine1 self) => self.Value;

        public static AddressLine1 FromString(string address)
        {
            CheckValidity(address);
            return new AddressLine1(address);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The first address line is required.", nameof(value));
            }

            if (value.Length > 30)
            {
                throw new ArgumentOutOfRangeException("Address line can not be longer than 30 characters.", nameof(value));
            }
        }
    }
}