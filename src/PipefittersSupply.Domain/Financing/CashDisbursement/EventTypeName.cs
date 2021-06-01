using System;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.Financing.CashDisbursement
{
    public class EventTypeName : Value<EventTypeName>
    {
        public string Value { get; }

        protected EventTypeName() { }

        internal EventTypeName(string value) => Value = value;

        public static implicit operator string(EventTypeName self) => self.Value;

        public static EventTypeName FromString(string value)
        {
            CheckValidity(value);
            return new EventTypeName(value);
        }

        private static void CheckValidity(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The event type name is required.", nameof(value));
            }

            if (value.Length > 25)
            {
                throw new ArgumentOutOfRangeException("The event type name can not be longer than 25 characters.", nameof(value));
            }
        }
    }
}