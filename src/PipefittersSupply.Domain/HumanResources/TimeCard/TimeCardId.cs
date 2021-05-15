using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.TimeCard
{
    public class TimeCardId : Value<TimeCardId>
    {
        private readonly int _value;

        public TimeCardId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("TimeCard Id must be specified", nameof(value));
            }

            _value = value;
        }
    }
}