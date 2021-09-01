using System;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public static class QueryParameters
    {
        public class GetEmployees
        {
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetEmployeesSupervisedBy
        {
            public Guid SupervisorID { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetEmployeesOfEmployeeType
        {
            public Guid EmployeeTypeID { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetEmployee
        {
            public Guid EmployeeID { get; set; }
        }
    }
}