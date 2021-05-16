using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class AddressLine1 : Value<AddressLine1>
    {
        private readonly string _value;

        private AddressLine1(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The first address line is required.", nameof(value));
            }

            if (value.Length > 30)
            {
                throw new ArgumentOutOfRangeException("Address line can not be longer than 30 characters.", nameof(value));
            }

            _value = value;
        }

        public static AddressLine1 FromString(string address) => new AddressLine1(address);
    }
}