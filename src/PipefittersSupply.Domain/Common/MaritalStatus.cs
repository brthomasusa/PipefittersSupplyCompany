using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class MaritalStatus : Value<MaritalStatus>
    {
        private readonly string _value;

        private MaritalStatus(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The marital status is required.", nameof(value));
            }

            if (value.ToUpper() != "M" && value.ToUpper() != "S")
            {
                throw new ArgumentException("Invalid marital status, valid statues are 'S' and 'M'.", nameof(value));
            }

            _value = value.ToUpper();
        }

        public static MaritalStatus FromString(string status) => new MaritalStatus(status);
    }
}