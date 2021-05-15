using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources
{
    public class SupervisorId : Value<SupervisorId>
    {
        private readonly int _value;

        public SupervisorId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Supervisor Id must be specified", nameof(value));
            }

            _value = value;
        }
    }
}