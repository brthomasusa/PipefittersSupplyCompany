using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class EmployeeStartDate : Value<EmployeeStartDate>
    {
        public DateTime Value { get; }

        protected EmployeeStartDate() { }


        internal EmployeeStartDate(DateTime value) => Value = value;

        public static implicit operator DateTime(EmployeeStartDate self) => self.Value;

        public static EmployeeStartDate FromDateTime(DateTime startDate)
        {
            CheckValidity(startDate);
            return new EmployeeStartDate(startDate);
        }

        private static void CheckValidity(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The employee start date is required.", nameof(value));
            }
        }
    }
}
