using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class CreatedDate : Value<CreatedDate>
    {
        public DateTime Value { get; }

        private CreatedDate(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The employee creation date is required.", nameof(value));
            }

            Value = value;
        }

        public static implicit operator DateTime(CreatedDate self) => self.Value;

        public static CreatedDate FromDateTime(DateTime createdDate) => new CreatedDate(createdDate);
    }
}