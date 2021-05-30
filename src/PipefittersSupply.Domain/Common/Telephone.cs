using System;
using System.Text.RegularExpressions;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Common
{
    public class Telephone : Value<Telephone>
    {
        public string Value { get; }

        internal Telephone(string value) => Value = value;

        public static implicit operator string(Telephone self) => self.Value;

        public static Telephone FromString(string phone)
        {
            CheckValidity(phone);
            return new Telephone(phone);
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