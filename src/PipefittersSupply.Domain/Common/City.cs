using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class City : Value<City>
    {
        private readonly string _value;

        private City(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("A city name is required.", nameof(value));
            }

            if (value.Length > 30)
            {
                throw new ArgumentOutOfRangeException("City name can not be longer than 30 characters.", nameof(value));
            }

            _value = value;
        }

        public static City FromString(string city) => new City(city);
    }
}