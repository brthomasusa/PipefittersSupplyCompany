using System;
using System.Text.RegularExpressions;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class SSN : ValueObject
    {
        public string Value { get; }

        protected SSN() { }

        private SSN(string phoneNumber)
            : this()
        {
            Value = phoneNumber;
        }

        public static implicit operator string(SSN self) => self.Value;

        public static SSN Create(string phoneNumber)
        {
            CheckValidity(phoneNumber);
            return new SSN(phoneNumber);
        }

        private static void CheckValidity(string value)
        {
            string rgTelephone = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The telephone number is required.", nameof(value));
            }

            if (!Regex.IsMatch(value, rgTelephone))
            {
                throw new ArgumentException("Invalid telephone number!", nameof(value));
            }
        }
    }
}