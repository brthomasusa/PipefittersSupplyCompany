using System;
using System.Text.RegularExpressions;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class Zipcode : Value<Zipcode>
    {
        public string Value { get; }
        internal Zipcode(string value)
        {
            CheckValidity(value);
            Value = value;
        }

        public static implicit operator string(Zipcode self) => self.Value;

        public static Zipcode FromString(string zipcode) => new Zipcode(zipcode);

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