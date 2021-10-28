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

        public EconomicEvent()
        {

        }
    }
}