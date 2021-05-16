using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class EmployeeMiddleInitial : Value<EmployeeMiddleInitial>
    {
        private readonly string _value;

        private EmployeeMiddleInitial(string value)
        {
            if (value.Length < 1 || value.Length > 1)
            {
                throw new ArgumentOutOfRangeException("Middle initial is limited to 1 character.", nameof(value));
            }

            _value = value.ToUpper();
        }

        public static EmployeeMiddleInitial FromString(string middleInitial) => new EmployeeMiddleInitial(middleInitial);
    }
}