using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class TaxExemption : Value<TaxExemption>
    {
        public int Value { get; }

        internal TaxExemption(int value) => Value = value;

        public static implicit operator int(TaxExemption self) => self.Value;

        public static TaxExemption FromInterger(int exemptions)
        {
            CheckValidity(exemptions);
            return new TaxExemption(exemptions);
        }

        private static void CheckValidity(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Number of exemptions must be zero or greater!", nameof(value));
            }
        }
    }
}
