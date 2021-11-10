using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.FinancierAggregate
{
    public class FinancierId : ValueObject
    {

        public Guid Value { get; }

        protected FinancierId() { }

        private FinancierId(Guid financierId)
            : this()
        {
            Value = financierId;
        }

        public static implicit operator Guid(FinancierId self) => self.Value;

        public static FinancierId Create(Guid financierId)
        {
            CheckValidity(financierId);
            return new FinancierId(financierId);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The financier Id is required; it can not be a default Guid.", nameof(value));
            }
        }
    }
}