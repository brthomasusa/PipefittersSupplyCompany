using System;

namespace PipefittersSupply.Contracts.HumanResources
{
    public static class TimeCardCommand
    {
        public static class V1
        {
            public class CreateTimeCard
            {
                public int Id { get; set; }
                public int EmployeeId { get; set; }
                public int SupervisorId { get; set; }
                public DateTime PayPeriodEnded { get; set; }
                public int RegularHours { get; set; }
                public int OvertimeHours { get; set; }
            }

            public class UpdateEmployeeId
            {
                public int Id { get; set; }
                public int EmployeeId { get; set; }
            }

            public class UpdatTimeCardSupervisorId
            {
                public int Id { get; set; }
                public int SupervisorId { get; set; }
            }

            public class UpdatePayPeriodEndDate
            {
                public int Id { get; set; }
                public DateTime PayPeriodEnded { get; set; }
            }

            public class UpdateRegularHours
            {
                public int Id { get; set; }
                public int RegularHours { get; set; }
            }

            public class UpdateOvertimeHours
            {
                public int Id { get; set; }
                public int OvertimeHours { get; set; }
            }
        }
    }
}