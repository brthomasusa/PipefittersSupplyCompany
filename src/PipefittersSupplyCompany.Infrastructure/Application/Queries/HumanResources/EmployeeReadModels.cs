using System;
using System.Collections.Generic;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public class EmployeeDependencyCheckResult : IReadModel
    {
        public Guid EmployeeId { get; set; }
        public int Addresses { get; set; }
        public int Contacts { get; set; }
    }

    public class SupervisorLookup : IReadModel
    {
        public Guid ManagerId { get; set; }
        public string ManagerName { get; set; }
    }

    public class EmployeeDetail : IReadModel
    {
        public Guid EmployeeId { get; set; }
        public Guid SupervisorId { get; set; }
        public string SupervisorFullName { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string EmployeeFullName { get; set; }
        public string SSN { get; set; }
        public string Telephone { get; set; }
        public string MaritalStatus { get; set; }
        public int Exemptions { get; set; }
        public decimal PayRate { get; set; }
        public DateTime StartDate { get; set; }
        public bool IsActive { get; set; }
        public List<EmployeeAddressDetail> Addresses { get; set; }
        public List<EmployeeContactDetail> Contacts { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }

    public class EmployeeListItem
    {
        public Guid EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string EmployeeFullName { get; set; }
        public string Telephone { get; set; }
        public bool IsActive { get; set; }
        public Guid SupervisorId { get; set; }
        public string ManagerLastName { get; set; }
        public string ManagerFirstName { get; set; }
        public string ManagerMiddleInitial { get; set; }
        public string SupervisorFullName { get; set; }
    }

    public class EmployeeListItemWithRoles
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

    public class EmployeeAddressListItem
    {
        public Guid EmployeeId { get; set; }
        public int AddressId { get; set; }
        public string FullAddress { get; set; }
    }

    public class EmployeeAddressDetail : IReadModel
    {
        public Guid EmployeeId { get; set; }
        public int AddressId { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string City { get; set; }
        public string StateCode { get; set; }
        public string Zipcode { get; set; }
        public string FullAddress { get; set; }
    }

    public class EmployeeContactDetail : IReadModel
    {
        public int PersonId { get; set; }
        public Guid EmployeeId { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public string MiddleInitial { get; set; }
        public string Telephone { get; set; }
        public string Notes { get; set; }
        public string FullName { get; set; }
    }

    public class EmployeeContactListItem
    {
        public int PersonId { get; set; }
        public Guid EmployeeId { get; set; }
        public string FullName { get; set; }
    }
}