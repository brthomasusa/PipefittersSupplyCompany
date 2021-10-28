using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public abstract class CashAccountTransaction : Entity<int>
    {
        protected DateTime transactionDate;
        protected decimal transactionAmount;
        protected int cashAccountID;
        protected int eventID;

        protected CashAccountTransaction() { }
    }
}