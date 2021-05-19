using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class City : Value<City>
    {
        public string Value { get; }

        internal City(string value) => Value = value;

        public static implicit operator string(City self) => self.Value;

        public static City FromString(string city)
        {
            CheckValidity(city);
            return new City(city);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("A city name is required.", nameof(value));
            }

            if (value.Length > 30)
            {
                throw new ArgumentOutOfRangeException("City name can not be longer than 30 characters.", nameof(value));
            }
        }
    }
}