using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class EmployeeMiddleInitial : Value<EmployeeMiddleInitial>
    {
        public string Value { get; }

        internal EmployeeMiddleInitial(string value) => Value = value.ToUpper();

        public static implicit operator string(EmployeeMiddleInitial self) => self.Value;

        public static EmployeeMiddleInitial FromString(string middleInitial)
        {
            CheckValidity(middleInitial);
            return new EmployeeMiddleInitial(middleInitial);
        }

        private static void CheckValidity(string value)
        {
            if (value.Length < 1 || value.Length > 1)
            {
                throw new ArgumentOutOfRangeException("Middle initial is limited to 1 character.", nameof(value));
            }
        }
    }
}
