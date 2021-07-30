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
            if (!Regex.IsMatch(value, @"^(?!219099999|078051120)(?!666|000|9\d{2})\d{3}(?!00)\d{2}(?!0{4})\d{4}$"))
            {
                throw new ArgumentException("Invalid social security number!", nameof(value));
            }
        }
    }
}