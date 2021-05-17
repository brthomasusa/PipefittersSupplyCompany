using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class LastModifiedDate : Value<LastModifiedDate>
    {

        private readonly DateTime _value;

        private LastModifiedDate(DateTime value)
        {
            _value = value;
        }

        public static LastModifiedDate FromDateTime(DateTime modifiedDate) => new LastModifiedDate(modifiedDate);
    }
}