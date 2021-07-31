using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel.Interfaces;

namespace PipefittersSupplyCompany.SharedKernel
{
    public abstract class AggregateRoot : IInternalEventHandler
    {
        private readonly List<BaseDomainEvent> _changes;

        protected AggregateRoot() => _changes = new List<BaseDomainEvent>();

        public Guid Id { get; protected set; }

        public DateTime CreatedDate { get; protected set; }

        public DateTime LastModifiedDate { get; protected set; }

        protected void Apply(BaseDomainEvent @event)
        {
            // Use Event object to set/update entity properties
            When(@event);

            // Run validation check to whole entity
            EnsureValidState();

            // Add Event object to List for later processing
            _changes.Add(@event);
        }

        protected abstract void When(BaseDomainEvent @event);

        public IEnumerable<BaseDomainEvent> GetChanges() => _changes.AsEnumerable();

        public void ClearChanges() => _changes.Clear();

        protected abstract void EnsureValidState();

        protected void ApplyToEntity(IInternalEventHandler entity, BaseDomainEvent @event) => entity?.Handle(@event);

        void IInternalEventHandler.Handle(BaseDomainEvent @event) => When(@event);
    }
}