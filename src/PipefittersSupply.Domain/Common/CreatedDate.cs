using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Common
{
    public class CreatedDate : Value<CreatedDate>
    {
        public DateTime Value { get; }

        protected CreatedDate() { }


        internal CreatedDate(DateTime value) => Value = value;

        public static implicit operator DateTime(CreatedDate self) => self.Value;

        public static CreatedDate FromDateTime(DateTime createdDate)
        {
            CheckValidity(createdDate);
            return new CreatedDate(createdDate);
        }

        private static void CheckValidity(DateTime endDate)
        {
            if (endDate == default)
            {
                throw new ArgumentNullException("The creation date, if set, can not be null.", nameof(endDate));
            }
        }
    }
}