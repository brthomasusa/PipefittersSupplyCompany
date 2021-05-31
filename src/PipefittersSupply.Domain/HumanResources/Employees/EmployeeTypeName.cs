using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class EmployeeTypeName : Value<EmployeeTypeName>
    {
        public string Value { get; }

        internal EmployeeTypeName(string value) => Value = value;

        public static implicit operator string(EmployeeTypeName self) => self.Value;

        public static EmployeeTypeName FromString(string value)
        {
            CheckValidity(value);
            return new EmployeeTypeName(value);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The employee type name is required.", nameof(value));
            }

            if (value.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Employee type name can not be longer than 25 characters.", nameof(value));
            }
        }
    }
}