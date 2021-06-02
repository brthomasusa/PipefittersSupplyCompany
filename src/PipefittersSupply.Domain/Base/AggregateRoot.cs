using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupply.Domain.Interfaces;

namespace PipefittersSupply.Domain.Base
{
    public abstract class AggregateRoot<TId> : IInternalEventHandler where TId : Value<TId>
    {
        private readonly List<object> _changes;

        protected AggregateRoot() => _changes = new List<object>();

        public TId Id { get; protected set; }

        protected void Apply(object @event)
        {
            // Use Event object to set/update entity properties
            When(@event);

            // Run validation check to whole entity
            EnsureValidState();

            // Add Event object to List for later processing
            _changes.Add(@event);
        }

        protected abstract void When(object @event);

        public IEnumerable<object> GetChanges() => _changes.AsEnumerable();

        public void ClearChanges() => _changes.Clear();

        protected abstract void EnsureValidState();

        protected void ApplyToEntity(IInternalEventHandler entity, object @event) => entity?.Handle(@event);

        void IInternalEventHandler.Handle(object @event) => When(@event);
    }
}