using System;
using PipefittersSupplyCompany.SharedKernel.Interfaces;

namespace PipefittersSupplyCompany.SharedKernel
{
    public abstract class Entity<T> : IInternalEventHandler
    {
        private readonly Action<object> _applier;

        protected Entity(Action<object> applier) => _applier = applier;

        protected Entity() { }

        public T Id { get; protected set; }

        public DateTime CreatedDate { get; protected set; }

        public DateTime? LastModifiedDate { get; protected set; }

        protected abstract void When(BaseDomainEvent @event);

        protected void Apply(BaseDomainEvent @event)
        {
            When(@event);
            _applier(@event);
        }

        void IInternalEventHandler.Handle(BaseDomainEvent @event) => When(@event);
    }
}