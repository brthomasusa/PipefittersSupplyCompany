using System;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class EconomicEvent : AggregateRoot<Guid>
    {
        private EventType _eventType;

        protected EconomicEvent() { }

        public EconomicEvent(Guid eventId, EventType eventType)
            : this()
        {
            if (eventId == default)
            {
                throw new ArgumentNullException("The event id is required.");
            }

            Id = eventId;
            EventType = eventType;
        }

        public EventType EventType
        {
            get { return _eventType; }

            private set
            {
                if (!Enum.IsDefined(typeof(EventType), value))
                {
                    throw new ArgumentException("Undefined event type.");
                }

                _eventType = value;
            }
        }

        // public virtual LoanAgreement LoanAgreement { get; private set; } <-- Uncomment this for 2-way navigation
        // public virtual LoanPayment LoanPayment { get; private set; }     <-- Uncomment this for 2-way navigation
    }
}