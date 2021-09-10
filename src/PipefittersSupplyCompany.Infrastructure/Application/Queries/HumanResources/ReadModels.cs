using System;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public static class ReadModels
    {
        public class EmployeeDetails
        {
            public Guid EmployeeId { get; set; }
            public Guid SupervisorId { get; set; }
            public string ManagerLastName { get; set; }
            public string ManagerFirstName { get; set; }
            public string ManagerMiddleInitial { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public string SSN { get; set; }
            public string Telephone { get; set; }
            public string MaritalStatus { get; set; }
            public int Exemptions { get; set; }
            public decimal PayRate { get; set; }
            public DateTime StartDate { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class EmployeeListItems
        {
            public Guid EmployeeId { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public string Telephone { get; set; }
            public bool IsActive { get; set; }
            public Guid SupervisorId { get; set; }
            public string ManagerLastName { get; set; }
            public string ManagerFirstName { get; set; }
            public string ManagerMiddleInitial { get; set; }
        }

        public class EmployeeListItemsWithRoles
        {
            public Guid EmployeeId { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public string Telephone { get; set; }
            public bool IsActive { get; set; }
            public Guid RoleId { get; set; }
            public string Role { get; set; }
            public Guid SupervisorId { get; set; }
            public string ManagerLastName { get; set; }
            public string ManagerFirstName { get; set; }
            public string ManagerMiddleInitial { get; set; }
        }

        public class EmployeeAddressDetails
        {
            public Guid EmployeeId { get; set; }
            public int AddressId { get; set; }
        }

        public class EmployeeAddressListItems
        {
            public Guid EmployeeId { get; set; }
        }

        public class EmployeeContactPersonDetails
        {
            public Guid EmployeeId { get; set; }
        }

        public class EmployeeContactPersonListItems
        {
            public Guid EmployeeId { get; set; }
        }
    }
}