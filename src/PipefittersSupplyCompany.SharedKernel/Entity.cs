using System;
using System.Collections.Generic;

namespace PipefittersSupplyCompany.SharedKernel
{
    public abstract class Entity<T>
    {
        public T Id { get; protected set; }

        public DateTime CreatedDate { get; protected set; }

        public DateTime? LastModifiedDate { get; protected set; }

        protected virtual void CheckValidity()
        {
            // Validation involving multiple properties go here.
        }
    }
}