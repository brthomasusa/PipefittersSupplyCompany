using System;

namespace PipefittersSupply.Domain.HumanResources
{
    public class TimeCard
    {
        public Guid Id { get; private set; }

        private Guid _employeeId;
        private Guid _supervisorId;
        private DateTime _payPeriodEnded;
        private int _regularHours;
        private int _overtimeHours;
        private DateTime _createdDate;
        private DateTime _lastModifiedDate;
    }
}