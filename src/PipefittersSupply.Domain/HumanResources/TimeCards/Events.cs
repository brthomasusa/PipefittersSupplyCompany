using System;

namespace PipefittersSupply.Domain.HumanResources.TimeCards
{
    public static class Events
    {
        public class TimeCardCreated
        {
            public int Id { get; set; }
            public int EmployeeId { get; set; }
            public int SupervisorId { get; set; }
            public DateTime PayPeriodEnded { get; set; }
            public int RegularHours { get; set; }
            public int OvertimeHours { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class EmployeeIdUpdated
        {
            public int Id { get; set; }
            public int EmployeeId { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class SupervisorIdUpdated
        {
            public int Id { get; set; }
            public int SupervisorId { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class PayPeriodEndDateUpdated
        {
            public int Id { get; set; }
            public DateTime PayPeriodEnded { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class RegularHoursUpdated
        {
            public int Id { get; set; }
            public int RegularHours { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class OvertimeHoursUpdated
        {
            public int Id { get; set; }
            public int OvertimeHours { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }
    }
}
