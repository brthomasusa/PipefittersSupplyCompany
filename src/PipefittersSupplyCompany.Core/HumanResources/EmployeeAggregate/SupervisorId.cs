using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class SupervisorId : ValueObject
    {
        public Guid Value { get; }

        protected SupervisorId() { }

        private SupervisorId(Guid id)
            : this()
        {
            Value = id;
        }

        public static implicit operator Guid(SupervisorId self) => self.Value;

        public static SupervisorId Create(Guid id)
        {
            CheckValidity(id);
            return new SupervisorId(id);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The supervisor id is required.");
            }
        }
    }
}