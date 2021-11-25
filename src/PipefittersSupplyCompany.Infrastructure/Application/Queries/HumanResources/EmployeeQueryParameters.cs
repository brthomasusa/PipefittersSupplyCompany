using System;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public class DoEmployeeDependencyCheck
    {
        public Guid EmployeeID { get; set; }
    }

    public class GetEmployee
    {
        public Guid EmployeeID { get; set; }
    }

    public class GetEmployees
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public string EmployeeLastName { get; set; }
        public string SupervisorLastName { get; set; }
        public string SortField { get; set; }
        public string SortOrder { get; set; } = "ASC";
    }

    public class GetEmployeesSupervisedBy
    {
        public Guid SupervisorID { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetEmployeesOfRole
    {
        public Guid RoleID { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetEmployeeAddress
    {
        public int AddressID { get; set; }
    }

    public class GetEmployeeAddresses
    {
        public Guid EmployeeID { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }

    public class GetEmployeeContact
    {
        public int PersonID { get; set; }
    }

    public class GetEmployeeContacts
    {
        public Guid EmployeeID { get; set; }
        public int Page { get; set; }
        public int PageSize { get; set; }
    }
}