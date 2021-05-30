using System;
using System.Text.RegularExpressions;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Common
{
    public class Zipcode : Value<Zipcode>
    {
        public string Value { get; }
        internal Zipcode(string value) => Value = value;

        public static implicit operator string(Zipcode self) => self.Value;

        public static Zipcode FromString(string zipcode)
        {
            CheckValidity(zipcode);
            return new Zipcode(zipcode);
        }

        private static void CheckValidity(string value)
        {
            var usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            var caZipRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

            if (!Regex.IsMatch(value, usZipRegEx) && !Regex.IsMatch(value, caZipRegEx))
            {
                throw new ArgumentException("Invalid zip code!", nameof(value));
            }
        }
    }
}