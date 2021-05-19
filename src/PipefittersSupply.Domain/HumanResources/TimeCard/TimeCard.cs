using System;
using PipefittersSupply.Framework;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employee;

namespace PipefittersSupply.Domain.HumanResources.TimeCard
{
    public class TimeCard : Entity<TimeCardId>
    {
        public TimeCardId Id { get; private set; }

        public EmployeeId EmployeeId { get; private set; }

        public EmployeeId SupervisorId { get; private set; }

        public PayPeriodEndDate PayPeriodEnded { get; private set; }

        public RegularHours RegularHours { get; private set; }

        public OvertimeHours OvertimeHours { get; private set; }

        public CreatedDate CreatedDate { get; private set; }

        public LastModifiedDate LastModifiedDate { get; private set; }

        public TimeCard
        (
            TimeCardId id,
            EmployeeId employeeID,
            EmployeeId supervisorID,
            PayPeriodEndDate periodEndDate,
            RegularHours regularHrs,
            OvertimeHours overtime
        ) =>
        Apply(new Events.TimeCardCreated
        {
            Id = id,
            EmployeeId = employeeID,
            SupervisorId = supervisorID,
            PayPeriodEnded = periodEndDate,
            RegularHours = regularHrs,
            OvertimeHours = overtime
        });

        protected override void EnsureValidState()
        {
            var valid = Id != null && EmployeeId != null && SupervisorId != null;

            if (!valid)
            {
                throw new InvalidEntityStateException(this, "Post-checks failed!");
            }
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.TimeCardCreated evt:
                    Id = new TimeCardId(evt.Id);
                    EmployeeId = new EmployeeId(evt.EmployeeId);
                    SupervisorId = new EmployeeId(evt.SupervisorId);
                    PayPeriodEnded = new PayPeriodEndDate(evt.PayPeriodEnded);
                    RegularHours = new RegularHours(evt.RegularHours);
                    OvertimeHours = new OvertimeHours(evt.OvertimeHours);
                    break;
            }
        }

    }
}