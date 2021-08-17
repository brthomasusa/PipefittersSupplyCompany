using System;
using System.Text.RegularExpressions;

namespace PipefittersSupplyCompany.SharedKernel.CommonValueObjects
{
    public class PhoneNumber : ValueObject
    {
        public string Value { get; }

        protected PhoneNumber() { }

        private PhoneNumber(string phoneNumber)
            : this()
        {
            Value = phoneNumber;
        }

        public static implicit operator string(PhoneNumber self) => self.Value;

        public static PhoneNumber Create(string phoneNumber)
        {
            CheckValidity(phoneNumber);
            return new PhoneNumber(phoneNumber);
        }

        private static void CheckValidity(string value)
        {
            string rgPhoneNumber = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The PhoneNumber number is required.", nameof(value));
            }

            if (!Regex.IsMatch(value, rgPhoneNumber))
            {
                throw new ArgumentException("Invalid PhoneNumber number!", nameof(value));
            }
        }
    }
}