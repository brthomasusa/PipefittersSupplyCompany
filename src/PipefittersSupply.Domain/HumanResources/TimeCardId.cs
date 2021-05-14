using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources
{
    public class TimeCardId : Value<TimeCardId>
    {
        private readonly Guid _value;

        public TimeCardId(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentException("TimeCard Id must be specified", nameof(value));
            }

            _value = value;
        }
    }
}