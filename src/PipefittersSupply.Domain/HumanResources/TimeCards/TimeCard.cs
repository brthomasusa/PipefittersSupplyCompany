using System;
using PipefittersSupply.Domain.Base;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.HumanResources.Employees;

namespace PipefittersSupply.Domain.HumanResources.TimeCards
{
    public class TimeCard : AggregateRoot<TimeCardId>
    {
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

        private string DbId
        {
            get => $"Employee/{Id}";
            set { }
        }

        public int TimeCardId { get; private set; }
        protected TimeCard() { }

        public EmployeeId EmployeeId { get; private set; }
        public void UpdateEmployeeId(EmployeeId value) =>
            Apply(new Events.EmployeeIdUpdated
            {
                Id = Id,
                EmployeeId = EmployeeId,
                LastModifiedDate = LastModifiedDate
            });

        public EmployeeId SupervisorId { get; private set; }
        public void UpdateSupervisorId(EmployeeId value) =>
            Apply(new Events.SupervisorIdUpdated
            {
                Id = Id,
                SupervisorId = SupervisorId,
                LastModifiedDate = LastModifiedDate
            });

        public PayPeriodEndDate PayPeriodEnded { get; private set; }
        public void UpdatePayPeriodEnded(PayPeriodEndDate value) =>
            Apply(new Events.PayPeriodEndDateUpdated
            {
                Id = Id,
                PayPeriodEnded = PayPeriodEnded,
                LastModifiedDate = LastModifiedDate
            });

        public RegularHours RegularHours { get; private set; }
        public void UpdateRegularHours(RegularHours value) =>
            Apply(new Events.RegularHoursUpdated
            {
                Id = Id,
                RegularHours = RegularHours,
                LastModifiedDate = LastModifiedDate
            });

        public OvertimeHours OvertimeHours { get; private set; }
        public void UpdateOvertimeHours(OvertimeHours value) =>
            Apply(new Events.OvertimeHoursUpdated
            {
                Id = Id,
                OvertimeHours = OvertimeHours,
                LastModifiedDate = LastModifiedDate
            });

        public CreatedDate CreatedDate { get; private set; }

        public LastModifiedDate LastModifiedDate { get; private set; }

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
                    CreatedDate = new CreatedDate(DateTime.Now);
                    TimeCardId = evt.Id;
                    break;
                case Events.EmployeeIdUpdated evt:
                    EmployeeId = new EmployeeId(evt.EmployeeId);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.SupervisorIdUpdated evt:
                    SupervisorId = new EmployeeId(evt.SupervisorId);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.PayPeriodEndDateUpdated evt:
                    PayPeriodEnded = new PayPeriodEndDate(evt.PayPeriodEnded);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.RegularHoursUpdated evt:
                    RegularHours = new RegularHours(evt.RegularHours);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.OvertimeHoursUpdated evt:
                    OvertimeHours = new OvertimeHours(evt.OvertimeHours);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
            }
        }
    }
}
