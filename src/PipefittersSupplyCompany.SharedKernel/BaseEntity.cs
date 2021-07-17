using System.Collections.Generic;

namespace PipefittersSupplyCompany.SharedKernel
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}