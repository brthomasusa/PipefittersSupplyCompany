using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class EmployeePayRate : Value<EmployeePayRate>
    {
        public decimal Value { get; }

        internal EmployeePayRate(decimal value) => Value = value;

        public static implicit operator decimal(EmployeePayRate self) => self.Value;

        public static EmployeePayRate FromDecimal(decimal rate)
        {
            CheckValidity(rate);
            return new EmployeePayRate(rate);
        }

        private static void CheckValidity(decimal value)
        {
            if (value < 7.50M || value > 40.00M)
            {
                throw new ArgumentException("Invalid pay rate, must be between $7.50 and $40.00 (per hour) inclusive!", nameof(value));
            }
        }
    }
}
