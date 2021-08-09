using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public abstract class CashTransaction : Entity<int>
    {
        protected DateTime transactionDate;
        protected decimal transactionAmount;
        protected int cashAccountID;
        protected int eventID;

    }
}