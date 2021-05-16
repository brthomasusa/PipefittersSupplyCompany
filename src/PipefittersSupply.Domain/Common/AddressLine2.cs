using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class AddressLine2 : Value<AddressLine2>
    {
        private readonly string _value;

        private AddressLine2(string value)
        {
            if (value.Length > 30)
            {
                throw new ArgumentOutOfRangeException("Address line can not be longer than 30 characters.", nameof(value));
            }

            _value = value;
        }

        public static AddressLine2 FromString(string address) => new AddressLine2(address);
    }
}