using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources
{
    public class TimeCardId : Value<TimeCardId>
    {
        private readonly Guid _value;

        public TimeCardId(Guid value) => _value = value;
    }
}