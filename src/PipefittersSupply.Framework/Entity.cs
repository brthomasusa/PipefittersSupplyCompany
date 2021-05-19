using System;
using System.Collections.Generic;
using System.Linq;

namespace PipefittersSupply.Framework
{
    public abstract class Entity<TId> where TId : IEquatable<TId>
    {
        private readonly List<object> _events;

        protected Entity() => _events = new List<object>();

        protected void Apply(object @event)
        {
            // Use Event object to set/update entity properties
            When(@event);

            // Run validation check to whole entity
            EnsureValidState();

            // Add Event object to List for later processing
            _events.Add(@event);
        }

        protected abstract void When(object @event);

        public IEnumerable<object> GetChanges() => _events.AsEnumerable();

        public void ClearChanges() => _events.Clear();

        protected abstract void EnsureValidState();
    }
}