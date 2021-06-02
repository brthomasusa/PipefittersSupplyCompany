using System;
using System.Collections.Generic;
using PipefittersSupply.Domain.Base;
using PipefittersSupply.Domain.Common;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class EmployeeType : AggregateRoot<EmployeeTypeIdentifier>
    {
        public int EmployeeTypeId { get; private set; }

        protected EmployeeType() { }

        public EmployeeType(EmployeeTypeIdentifier employeeTypeId, EmployeeTypeName typeName) =>
            Apply(new Events.EmployeeTypeCreated
            {
                Id = employeeTypeId,
                EmployeeTypeName = typeName
            });

        public EmployeeTypeName EmployeeTypeName { get; private set; }

        public CreatedDate CreatedDate { get; private set; }

        public List<Employee> Employees { get; } = new List<Employee>();

        public LastModifiedDate LastModifiedDate { get; private set; }

        protected override void EnsureValidState()
        {
            var valid = Id != null;

            if (!valid)
            {
                throw new InvalidEntityStateException(this, "Post-checks failed!");
            }
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.EmployeeTypeCreated evt:
                    Id = new EmployeeTypeIdentifier(evt.Id);
                    EmployeeTypeName = new EmployeeTypeName(evt.EmployeeTypeName);
                    CreatedDate = new CreatedDate(DateTime.Now);
                    EmployeeTypeId = evt.Id;
                    break;
            }
        }
    }
}