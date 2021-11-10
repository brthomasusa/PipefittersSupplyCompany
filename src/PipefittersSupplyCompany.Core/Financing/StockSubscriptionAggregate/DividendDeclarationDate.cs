using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate
{
    public class DividendDeclarationDate : ValueObject
    {
        public DateTime Value { get; }

        protected DividendDeclarationDate() { }

        private DividendDeclarationDate(DateTime dividendDeclarationDate)
            : this()
        {
            Value = dividendDeclarationDate;
        }

        public static implicit operator DateTime(DividendDeclarationDate self) => self.Value;

        public static DividendDeclarationDate Create(DateTime dividendDeclarationDate)
        {
            CheckValidity(dividendDeclarationDate);
            return new DividendDeclarationDate(dividendDeclarationDate);
        }

        private static void CheckValidity(DateTime value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The payment due date is required.", nameof(value));
            }
        }
    }
}