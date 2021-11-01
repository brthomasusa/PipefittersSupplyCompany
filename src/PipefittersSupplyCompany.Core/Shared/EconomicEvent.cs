using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
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
    }
}