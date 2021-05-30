using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Common
{
    public class LastModifiedDate : Value<LastModifiedDate>
    {

        public DateTime Value { get; }

        internal LastModifiedDate(DateTime value) => Value = value;

        public static implicit operator DateTime(LastModifiedDate self) => self.Value;

        public static LastModifiedDate FromDateTime(DateTime modifiedDate)
        {
            CheckValidity(modifiedDate);
            return new LastModifiedDate(modifiedDate);
        }

        private static void CheckValidity(DateTime endDate)
        {
            if (endDate == default)
            {
                throw new ArgumentNullException("The last modified date, if set, can not be null.", nameof(endDate));
            }
        }
    }
}