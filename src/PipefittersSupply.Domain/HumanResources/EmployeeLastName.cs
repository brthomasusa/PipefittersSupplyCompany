using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources
{
    public class EmployeeLastName : Value<EmployeeLastName>
    {
        private readonly string _value;

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

            _value = value;
        }

        public static EmployeeLastName FromString(string lastName) => new EmployeeLastName(lastName);
    }
}