using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class EmployeeLastName : Value<EmployeeLastName>
    {
        public string Value { get; }

        protected EmployeeLastName() { }

        internal EmployeeLastName(string value) => Value = value;

        public static implicit operator string(EmployeeLastName self) => self.Value;

        public static EmployeeLastName FromString(string lastName)
        {
            CheckValidity(lastName);
            return new EmployeeLastName(lastName);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The employee last name is required.", nameof(value));
            }

            if (value.Length > 25)
            {
                throw new ArgumentOutOfRangeException("Last name can not be longer than 25 characters.", nameof(value));
            }
        }
    }
}
