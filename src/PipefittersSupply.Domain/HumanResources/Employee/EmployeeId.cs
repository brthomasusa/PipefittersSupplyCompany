using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class EmployeeId : Value<EmployeeId>
    {
        private readonly int _value;

        public EmployeeId(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Employee Id must be specified", nameof(value));
            }

            _value = value;
        }

    }
}