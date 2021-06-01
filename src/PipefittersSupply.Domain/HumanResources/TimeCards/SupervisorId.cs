using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.TimeCards
{
    public class SupervisorId : Value<SupervisorId>
    {
        private readonly int _value;

        protected SupervisorId() { }

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
