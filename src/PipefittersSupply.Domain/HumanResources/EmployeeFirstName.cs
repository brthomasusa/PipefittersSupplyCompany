using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources
{
    public class EmployeeFirstName : Value<EmployeeFirstName>
    {
        private readonly string _value;

        private EmployeeFirstName(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The employee first name is required.", nameof(value));
            }

            if (value.Length > 25)
            {
                throw new ArgumentOutOfRangeException("First name can not be longer than 25 characters.", nameof(value));
            }

            _value = value;
        }

        public static EmployeeFirstName FromString(string firstName) => new EmployeeFirstName(firstName);
    }
}