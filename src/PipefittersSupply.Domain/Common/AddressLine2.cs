using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class AddressLine2 : Value<AddressLine2>
    {
        public string Value { get; }

        internal AddressLine2(string value)
        {
            CheckValidity(value);
            Value = value;
        }

        public static implicit operator string(AddressLine2 self) => self.Value;

        public static AddressLine2 FromString(string address) => new AddressLine2(address);

        private static void CheckValidity(string value)
        {
            if (value.Length > 30)
            {
                throw new ArgumentOutOfRangeException("Address line can not be longer than 30 characters.", nameof(value));
            }
        }
    }
}