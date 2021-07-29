using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class MaritalStatus : ValueObject
    {
        public string Value { get; }

        protected MaritalStatus() { }

        private MaritalStatus(string value) : this() => Value = value.ToUpper();

        public static implicit operator string(MaritalStatus self) => self.Value;

        public static MaritalStatus Create(string status)
        {
            CheckValidity(status);
            return new MaritalStatus(status);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The marital status is required.", nameof(value));
            }

            if (value.ToUpper() != "M" && value.ToUpper() != "S")
            {
                throw new ArgumentException("Invalid marital status, valid statues are 'S' and 'M'.", nameof(value));
            }
        }
    }
}