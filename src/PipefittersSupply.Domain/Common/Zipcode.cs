using System;
using System.Text.RegularExpressions;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class Zipcode : Value<Zipcode>
    {
        private readonly string _value;

        private Zipcode(string value)
        {
            var usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            var caZipRegEx = @"^([ABCEGHJKLMNPRSTVXY]\d[ABCEGHJKLMNPRSTVWXYZ])\ {0,1}(\d[ABCEGHJKLMNPRSTVWXYZ]\d)$";

            if (!Regex.IsMatch(value, usZipRegEx) && !Regex.IsMatch(value, caZipRegEx))
            {
                throw new ArgumentException("Invalid zip code!", nameof(value));
            }

            _value = value;
        }

        public static Zipcode FromString(string zipcode) => new Zipcode(zipcode);
    }
}