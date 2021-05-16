using System;
using System.Text.RegularExpressions;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class Telephone : Value<Telephone>
    {
        private readonly string _value;

        private Telephone(string value)
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

            _value = value;
        }

        public static Telephone FromString(string phone) => new Telephone(phone);
    }
}