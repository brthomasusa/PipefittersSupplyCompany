using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class CreatedDate : Value<CreatedDate>
    {
        private readonly DateTime _value;

        private CreatedDate(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The employee creation date is required.", nameof(value));
            }

            _value = value;
        }

        public static CreatedDate FromDateTime(DateTime createdDate) => new CreatedDate(createdDate);
    }
}