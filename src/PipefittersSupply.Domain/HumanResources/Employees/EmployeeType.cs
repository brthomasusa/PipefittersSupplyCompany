using System;
using PipefittersSupply.Domain.Base;
using PipefittersSupply.Domain.Common;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class EmployeeType : AggregateRoot<EmployeeTypeIdentifier>
    {
        public EmployeeType(EmployeeTypeIdentifier employeeTypeId, EmployeeTypeName typeName) =>
            Apply(new Events.EmployeeTypeIdCreated
            {
                Id = employeeTypeId,
                EmployeeTypeName = typeName
            });

        public int EmployeeTypeId { get; private set; }

        protected EmployeeType() { }

        public EmployeeTypeName EmployeeTypeName { get; private set; }

        public CreatedDate CreatedDate { get; private set; }

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
                case Events.EmployeeTypeIdCreated evt:
                    Id = new EmployeeTypeIdentifier(evt.Id);
                    EmployeeTypeName = new EmployeeTypeName(evt.EmployeeTypeName);
                    CreatedDate = new CreatedDate(DateTime.Now);
                    EmployeeTypeId = evt.Id;
                    break;
            }
        }
    }
}