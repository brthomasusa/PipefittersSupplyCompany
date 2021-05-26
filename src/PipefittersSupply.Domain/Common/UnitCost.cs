using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class UnitCost : Value<UnitCost>
    {
        public decimal Value { get; }

        internal UnitCost(decimal value) => Value = value;

        public static implicit operator decimal(UnitCost self) => self.Value;

        public static UnitCost FromDecimal(decimal value)
        {
            CheckValidity(value);
            return new UnitCost(value);
        }

        private static void CheckValidity(decimal value)
        {
            if (value < 0M)
            {
                throw new ArgumentException("Invalid unit cost, can not be a negative amount!", nameof(value));
            }
        }
    }
}