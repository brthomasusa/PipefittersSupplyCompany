using System;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class EconomicEvent
    {
        public Guid EventId { get; }

        public EconomicEvent()
        {
            EventId = Guid.NewGuid();
        }
    }
}