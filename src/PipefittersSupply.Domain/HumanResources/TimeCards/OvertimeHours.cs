using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.TimeCards
{
    public class OvertimeHours : Value<OvertimeHours>
    {
        public int Value { get; }

        protected OvertimeHours() { }

        internal OvertimeHours(int value) => Value = value;

        public static implicit operator int(OvertimeHours self) => self.Value;

        public static OvertimeHours FromInterger(int hours)
        {
            CheckValidity(hours);
            return new OvertimeHours(hours);
        }

        private static void CheckValidity(int hours)
        {
            if (hours < 0 || hours > 201)
            {
                throw new ArgumentException("Overtime hours must be between 0 and 201 hours.", nameof(hours));
            }
        }
    }
}
