using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class TaxExemption : ValueObject
    {
        public int Value { get; }

        protected TaxExemption() { }

        private TaxExemption(int value) : this() => Value = value;

        public static implicit operator int(TaxExemption self) => self.Value;

        public static TaxExemption Create(int exemptions)
        {
            CheckValidity(exemptions);
            return new TaxExemption(exemptions);
        }

        private static void CheckValidity(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Number of exemptions must be greater than or equal to zero.", nameof(value));
            }
        }
    }
}