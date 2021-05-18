using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class EmployeeStartDate : Value<EmployeeStartDate>
    {
        public DateTime Value { get; }

        private EmployeeStartDate(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The employee start date is required.", nameof(value));
            }

            Value = value;
        }

        public static implicit operator DateTime(EmployeeStartDate self) => self.Value;

        public static EmployeeStartDate FromDateTime(DateTime startDate) => new EmployeeStartDate(startDate);
    }
}