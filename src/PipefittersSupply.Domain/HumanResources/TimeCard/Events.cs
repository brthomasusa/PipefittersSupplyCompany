using System;

namespace PipefittersSupply.Domain.HumanResources.TimeCard
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
            public DateTime LastModifiedDate { get; set; }
        }
    }
}