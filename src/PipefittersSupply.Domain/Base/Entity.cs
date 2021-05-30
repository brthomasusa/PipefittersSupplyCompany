using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupply.Domain.Interfaces;

namespace PipefittersSupply.Domain.Base
{
    public abstract class Entity<TId> : IInternalEventHandler where TId : Value<TId>
    {
        private readonly Action<object> _applier;

        protected Entity(Action<object> applier) => _applier = applier;

        public TId Id { get; protected set; }

        protected abstract void When(object @event);

        protected void Apply(object @event)
        {
            When(@event);
            _applier(@event);
        }

        void IInternalEventHandler.Handle(object @event) => When(@event);
    }
}