using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources
{
    public class EmployeeTypeIdentifier
    {
        private readonly int _value;

        private EmployeeTypeIdentifier(int value)
        {
            if (value == default)
            {
                throw new ArgumentException("Employee type must be specified", nameof(value));
            }

            if (value < 0)
            {
                throw new ArgumentException("Invalid employee type; employee type can not be negative.", nameof(value));
            }

            _value = value;
        }

        public static EmployeeTypeIdentifier FromInterger(int employeeTypeID) => new EmployeeTypeIdentifier(employeeTypeID);
    }
}