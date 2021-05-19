using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class EmployeeMiddleInitial : Value<EmployeeMiddleInitial>
    {
        public string Value { get; }

        internal EmployeeMiddleInitial(string value)
        {
            CheckValidity(value);
            Value = value.ToUpper();
        }

        public static implicit operator string(EmployeeMiddleInitial self) => self.Value;

        public static EmployeeMiddleInitial FromString(string middleInitial) => new EmployeeMiddleInitial(middleInitial);

        private static void CheckValidity(string value)
        {
            if (value.Length < 1 || value.Length > 1)
            {
                throw new ArgumentOutOfRangeException("Middle initial is limited to 1 character.", nameof(value));
            }
        }
    }
}