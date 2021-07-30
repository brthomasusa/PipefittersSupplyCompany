using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class EmployeeID : ValueObject
    {
        public Guid Value { get; }

        protected EmployeeID() { }

        private EmployeeID(Guid id)
            : this()
        {
            Value = id;
        }

        public static implicit operator Guid(EmployeeID self) => self.Value;

        public static EmployeeID Create(Guid id)
        {
            CheckValidity(id);
            return new EmployeeID(id);
        }

        private static void CheckValidity(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("The employee id is required.", nameof(value));
            }
        }
    }
}