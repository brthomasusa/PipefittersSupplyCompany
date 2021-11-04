using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class EconomicEventId : ValueObject
    {
        public Guid Value { get; }

        protected EconomicEventId() { }

        private EconomicEventId(Guid eventID)
            : this()
        {
            Value = eventID;
        }

        public static implicit operator Guid(EconomicEventId self) => self.Value;

        public static EconomicEventId Create(Guid eventID)
        {
            CheckValidity(eventID);
            return new EconomicEventId(eventID);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The economic event Id is required; it can not be a default Guid.", nameof(value));
            }
        }
    }
}