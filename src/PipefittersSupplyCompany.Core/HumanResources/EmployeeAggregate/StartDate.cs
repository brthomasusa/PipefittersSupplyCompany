using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class StartDate : ValueObject
    {
        public DateTime Value { get; }

        protected StartDate() { }


        private StartDate(DateTime value) : this() => Value = value;

        public static implicit operator DateTime(StartDate self) => self.Value;

        public static StartDate Create(DateTime startDate)
        {
            CheckValidity(startDate);
            return new StartDate(startDate);
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