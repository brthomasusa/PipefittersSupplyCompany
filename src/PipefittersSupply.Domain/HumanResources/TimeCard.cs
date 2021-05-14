using System;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources
{
    public class TimeCard
    {
        private EmployeeId _employeeId;
        private EmployeeId _supervisorId;
        private DateTime _payPeriodEnded;
        private int _regularHours;
        private int _overtimeHours;
        private DateTime _createdDate;
        private DateTime _lastModifiedDate;
        public Guid Id { get; private set; }


        public TimeCard(Guid id, EmployeeId employeeID, EmployeeId supervisorID)
        {
            if (id == default)
            {
                throw new ArgumentException("Identity must be specified", nameof(id));
            }

            Id = id;
            _employeeId = employeeID;
            _supervisorId = supervisorID;
        }

        public void UpdatePayPeriodEnded(DateTime periodEnded) => _payPeriodEnded = periodEnded;

        public void UpdateRegularHours(int hrs) => _regularHours = hrs;

        public void UpdateOvertimeHours(int hrs) => _overtimeHours = hrs;

        public void UpdateCreatedDate(DateTime created) => _createdDate = created;

        public void UpdateLastModifiedDate(DateTime lastModified) => _lastModifiedDate = lastModified;
    }
}