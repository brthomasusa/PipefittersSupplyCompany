using System;
using PipefittersSupplyCompany.SharedKernel;
using System.Text.RegularExpressions;

namespace PipefittersSupplyCompany.Core.Financing.CashAccountAggregate
{
    public class RoutingTransitNumber : ValueObject
    {
        public string Value { get; }

        protected RoutingTransitNumber() { }

        private RoutingTransitNumber(string routingNumber)
            : this()
        {
            Value = routingNumber;
        }

        public static implicit operator string(RoutingTransitNumber self) => self.Value;

        public static RoutingTransitNumber Create(string routingNumber)
        {
            CheckValidity(routingNumber);
            return new RoutingTransitNumber(routingNumber);
        }

        private static void CheckValidity(string value)
        {
            string rgRoutingNumber = @"^((0[0-9])|(1[0-2])|(2[1-9])|(3[0-2])|(6[1-9])|(7[0-2])|80)([0-9]{7})$";

            if (string.IsNullOrEmpty(value))
            {
                throw new ArgumentNullException("The bank routing number is required.", nameof(value));
            }

            if (!Regex.IsMatch(value, rgRoutingNumber))
            {
                throw new ArgumentException("Invalid bank routing (Transit ABA) number!", nameof(value));
            }
        }
    }
}