using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.Financing
{
    public class Money : Value<Money>
    {
        public decimal Amount { get; }

        public Money(decimal amt) => Amount = amt;

    }
}