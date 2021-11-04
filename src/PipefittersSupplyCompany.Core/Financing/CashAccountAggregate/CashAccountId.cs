using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class CashAccountId : ValueObject
    {
        public Guid Value { get; }

        protected CashAccountId() { }

        private CashAccountId(Guid cashAcctID)
            : this()
        {
            Value = cashAcctID;
        }

        public static implicit operator Guid(CashAccountId self) => self.Value;

        public static CashAccountId Create(Guid cashAcctID)
        {
            CheckValidity(cashAcctID);
            return new CashAccountId(cashAcctID);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The cash account Id is required; it can not be a default Guid.", nameof(value));
            }
        }
    }
}