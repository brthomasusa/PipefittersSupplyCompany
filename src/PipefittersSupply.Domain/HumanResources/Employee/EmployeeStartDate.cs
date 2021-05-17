using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class EmployeeStartDate : Value<EmployeeStartDate>
    {
        private readonly DateTime _value;

        private EmployeeStartDate(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The employee start date is required.", nameof(value));
            }

            _value = value;
        }

        public static EmployeeStartDate FromDateTime(DateTime startDate) => new EmployeeStartDate(startDate);
    }
}