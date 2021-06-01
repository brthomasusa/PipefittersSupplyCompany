using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Common
{
    public class MaritalStatus : Value<MaritalStatus>
    {
        public string Value { get; }

        protected MaritalStatus() { }


        internal MaritalStatus(string value) => Value = value.ToUpper();

        public static implicit operator string(MaritalStatus self) => self.Value;

        public static MaritalStatus FromString(string status)
        {
            CheckValidity(status);
            return new MaritalStatus(status);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The marital status is required.", nameof(value));
            }

            if (value.ToUpper() != "M" && value.ToUpper() != "S")
            {
                throw new ArgumentException("Invalid marital status, valid statues are 'S' and 'M'.", nameof(value));
            }
        }
    }
}