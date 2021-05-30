using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.TimeCards
{
    public class RegularHours : Value<RegularHours>
    {
        public int Value { get; }

        internal RegularHours(int value) => Value = value;

        public static implicit operator int(RegularHours self) => self.Value;

        public static RegularHours FromInterger(int hours)
        {
            CheckValidity(hours);
            return new RegularHours(hours);
        }

        private static void CheckValidity(int hours)
        {
            if (hours < 0 || hours > 185)
            {
                throw new ArgumentException("Regular hours must be between 0 and 185 hours.", nameof(hours));
            }
        }
    }
}
