using System;
using System.Text.RegularExpressions;

namespace PipefittersSupplyCompany.SharedKernel.CommonValueObjects
{
    public class Telephone : ValueObject
    {
        public string Value { get; }

        protected Telephone() { }

        private Telephone(string phoneNumber)
            : this()
        {
            Value = phoneNumber;
        }

        public static implicit operator string(Telephone self) => self.Value;

        public static Telephone Create(string phoneNumber)
        {
            CheckValidity(phoneNumber);
            return new Telephone(phoneNumber);
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