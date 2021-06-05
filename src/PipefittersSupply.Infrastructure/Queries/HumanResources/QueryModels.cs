using System;

namespace PipefittersSupply.Infrastructure.Queries.HumanResources
{
    public static class QueryModels
    {
        public class GetEmployees
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetEmployeesSupervisedBy
        {
            public int SupervisorID { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetEmployeesOfEmployeeType
        {
            public int EmployeeTypeID { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetEmployee
        {
            public int EmployeeID { get; set; }
        }
    }
}