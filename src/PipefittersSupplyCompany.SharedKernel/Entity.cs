using System;
using System.Collections.Generic;

namespace PipefittersSupplyCompany.SharedKernel
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }

        public DateTime CreatedDate { get; }

        public DateTime? LastModifiedDate { get; private set; }

        public void UpdateLastModifiedDate()
        {
            LastModifiedDate = DateTime.UtcNow;
        }

        protected virtual void CheckValidity()
        {
            // Validation involving multiple properties go here.
        }
    }
}