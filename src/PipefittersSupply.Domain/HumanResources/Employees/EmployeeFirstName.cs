using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class EmployeeFirstName : Value<EmployeeFirstName>
    {
        public string Value { get; }

        internal EmployeeFirstName(string value) => Value = value;

        public static implicit operator string(EmployeeFirstName self) => self.Value;

        public static EmployeeFirstName FromString(string firstName)
        {
            CheckValidity(firstName);
            return new EmployeeFirstName(firstName);
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
