using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class BankName : ValueObject
    {
        public string Value { get; }

        protected BankName() { }

        private BankName(string acctName)
            : this()
        {
            Value = acctName;
        }

        public static implicit operator string(BankName self) => self.Value;

        public static BankName Create(string bankName)
        {
            CheckValidity(bankName);
            return new BankName(bankName);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The bank name is required.", nameof(value));
            }

            if (value.Length > 50)
            {
                throw new ArgumentException("The bank name maximum length is 50 characters.", nameof(value));
            }
        }
    }
}