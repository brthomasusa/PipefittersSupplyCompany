using System;
using System.Collections.Generic;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public static class ReadModels
    {
        public class EmployeeDetails : ReadModelBase, IReadModel
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

        public class EmployeeListItems : ReadModelBase
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

        public class EmployeeAddressListItems
        {
            public Guid EmployeeId { get; set; }
            public int AddressId { get; set; }
            public string FullAddress { get; set; }
        }

        public class EmployeeAddressDetails
        {
            public Guid EmployeeId { get; set; }
            public int AddressId { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string StateCode { get; set; }
            public string Zipcode { get; set; }
        }

        public class EmployeeContactDetails
        {
            public int PersonId { get; set; }
            public Guid EmployeeId { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public string Telephone { get; set; }
            public string Notes { get; set; }
        }

        public class EmployeeContactListItems
        {
            public int PersonId { get; set; }
            public Guid EmployeeId { get; set; }
            public string FullName { get; set; }
        }
    }
}