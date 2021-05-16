using System;
using System.Text.RegularExpressions;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Common
{
    public class StateProvinceCode : Value<StateProvinceCode>
    {
        private readonly string _value;

        private StateProvinceCode(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The 2-digit state (province) code is required.", nameof(value));
            }

            if (!Regex.IsMatch(value, "^[a-zA-Z]+$"))
            {
                throw new ArgumentException("The 2-digit state (province) code contains only letters.", nameof(value));
            }

            if (value.Length != 2)
            {
                throw new ArgumentOutOfRangeException("The state (province) code must be 2 characters.", nameof(value));
            }

            _value = value.ToUpper();
        }

        public static StateProvinceCode FromString(string stateCode) => new StateProvinceCode(stateCode);
    }
}