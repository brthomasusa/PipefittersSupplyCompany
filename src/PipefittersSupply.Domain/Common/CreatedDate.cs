using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class CreatedDate : Value<CreatedDate>
    {
        public DateTime Value { get; }

        internal CreatedDate(DateTime value)
        {
            CheckValidity(value);
            Value = value;
        }

        public static implicit operator DateTime(CreatedDate self) => self.Value;

        public static CreatedDate FromDateTime(DateTime createdDate) => new CreatedDate(createdDate);

        private static void CheckValidity(DateTime endDate)
        {
            if (endDate == default)
            {
                throw new ArgumentNullException("The creation date, if set, can not be null.", nameof(endDate));
            }
        }
    }
}