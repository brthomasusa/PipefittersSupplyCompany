using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class EmployeeTypeIdentifier : Value<EmployeeTypeIdentifier>
    {
        public int Value { get; }

        protected EmployeeTypeIdentifier() { }

        internal EmployeeTypeIdentifier(int value) => Value = value;

        public static implicit operator int(EmployeeTypeIdentifier self) => self.Value;

        public static EmployeeTypeIdentifier FromInterger(int employeeTypeID)
        {
            CheckValidity(employeeTypeID);
            return new EmployeeTypeIdentifier(employeeTypeID);
        }

        private static void CheckValidity(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Employee type must be specified.", nameof(value));
            }

            if (value < 0)
            {
                throw new ArgumentException("Invalid employee type; employee type can not be negative.", nameof(value));
            }
        }
    }
}
