using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class EmployeeTypeIdentifier
    {
        public int Value { get; }

        internal EmployeeTypeIdentifier(int value)
        {
            CheckValidity(value);
            Value = value;
        }

        public static implicit operator int(EmployeeTypeIdentifier self) => self.Value;

        public static EmployeeTypeIdentifier FromInterger(int employeeTypeID) => new EmployeeTypeIdentifier(employeeTypeID);

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