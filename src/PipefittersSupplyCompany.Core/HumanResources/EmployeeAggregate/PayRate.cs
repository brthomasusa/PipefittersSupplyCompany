using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class PayRate : ValueObject
    {
        public decimal Value { get; }

        protected PayRate() { }

        internal PayRate(decimal value) => Value = value;

        public static implicit operator decimal(PayRate self) => self.Value;

        public static PayRate Create(decimal rate)
        {
            CheckValidity(rate);
            return new PayRate(rate);
        }

        private static void CheckValidity(decimal value)
        {
            if (value == 0M)
            {
                throw new ArgumentException("Invalid pay rate; pay rate can not be zero!", nameof(value));
            }

            if (value < 7.50M || value > 40.00M)
            {
                throw new ArgumentException("Invalid pay rate, must be between $7.50 and $40.00 (per hour) inclusive!", nameof(value));
            }
        }
    }
}