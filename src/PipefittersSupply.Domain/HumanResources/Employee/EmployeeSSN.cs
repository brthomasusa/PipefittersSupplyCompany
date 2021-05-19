using System;
using System.Text.RegularExpressions;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class EmployeeSSN : Value<EmployeeSSN>
    {
        public string Value { get; }

        internal EmployeeSSN(string value)
        {
            CheckValidity(value);
            Value = value;
        }

        public static implicit operator string(EmployeeSSN self) => self.Value;

        public static EmployeeSSN FromString(string ssn) => new EmployeeSSN(ssn);

        private static void CheckValidity(string value)
        {
            if (!Regex.IsMatch(value, @"^(?!219099999|078051120)(?!666|000|9\d{2})\d{3}(?!00)\d{2}(?!0{4})\d{4}$"))
            {
                throw new ArgumentException("Invalid social security number!", nameof(value));
            }
        }
    }
}