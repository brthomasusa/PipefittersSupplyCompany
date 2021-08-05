using System;
using System.Collections.Generic;

namespace PipefittersSupplyCompany.SharedKernel
{
    public abstract class BaseEntity<TId>
    {
        public TId Id { get; set; }

        public DateTime CreatedDate { get; protected set; }

        public DateTime LastModifiedDate { get; protected set; }

        public List<BaseDomainEvent> Events = new List<BaseDomainEvent>();
    }
}