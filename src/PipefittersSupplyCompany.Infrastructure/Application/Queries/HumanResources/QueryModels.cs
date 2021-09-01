using System;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
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
            public Guid SupervisorId { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetEmployeesOfRole
        {
            public Guid RoleID { get; set; }
            public int Page { get; set; }
            public int PageSize { get; set; }
        }

        public class GetEmployee
        {

            public Guid EmployeeID { get; set; }
        }
    }
}