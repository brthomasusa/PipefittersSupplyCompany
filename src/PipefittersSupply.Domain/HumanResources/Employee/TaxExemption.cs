using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class TaxExemption : Value<TaxExemption>
    {
        public int Value { get; }

        internal TaxExemption(int value)
        {
            CheckValidity(value);
            Value = value;
        }

        public static implicit operator int(TaxExemption self) => self.Value;

        public static TaxExemption FromInterger(int exemptions) => new TaxExemption(exemptions);

        private static void CheckValidity(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Number of exemptions must be zero or greater!", nameof(value));
            }
        }
    }
}