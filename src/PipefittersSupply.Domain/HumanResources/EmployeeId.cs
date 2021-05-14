using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources
{
    public class EmployeeId : Value<EmployeeId>
    {
        private readonly Guid _value;

        public EmployeeId(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentException("Employee Id must be specified", nameof(value));
            }

            _value = value;
        }

    }
}