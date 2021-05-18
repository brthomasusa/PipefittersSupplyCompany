using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class EmployeeLastName : Value<EmployeeLastName>
    {
        public string Value { get; }

        private EmployeeLastName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The employee last name is required.", nameof(value));
            }

            if (value.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Last name can not be longer than 25 characters.", nameof(value));
            }

            Value = value;
        }

        public static implicit operator string(EmployeeLastName self) => self.Value;

        public static EmployeeLastName FromString(string lastName) => new EmployeeLastName(lastName);
    }
}