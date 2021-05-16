using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class TaxExemption : Value<TaxExemption>
    {
        private readonly int _value;

        private TaxExemption(int value)
        {
            if (value < 0)
            {
                throw new ArgumentException("Number of exemptions must be zero or greater!", nameof(value));
            }

            _value = value;
        }

        public static TaxExemption FromInterger(int exemptions) => new TaxExemption(exemptions);
    }
}