using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class EmployeePayRate : Value<EmployeePayRate>
    {
        private readonly decimal _value;

        private EmployeePayRate(decimal value)
        {
            if (value < 7.50M || value > 40.00M)
            {
                throw new ArgumentException("Invalid pay rate, must be between $7.50 and $40.00 (per hour) inclusive!", nameof(value));
            }

            _value = value;
        }

        public static EmployeePayRate FromDecimal(decimal rate) => new EmployeePayRate(rate);
    }
}