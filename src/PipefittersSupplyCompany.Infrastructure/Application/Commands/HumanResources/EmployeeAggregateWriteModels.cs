using System;
using PipefittersSupplyCompany.Core.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources
{
    public static class EmployeeAggregateCommand
    {
        public class CreateEmployeeInfo : IWriteModel
        {
            public Guid Id { get; set; }
            public Guid SupervisorId { get; set; }
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
        }

        public class EditEmployeeInfo : IWriteModel
        {
            public Guid Id { get; set; }
            public Guid SupervisorId { get; set; }
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
        }

        public class DeleteEmployeeInfo : IWriteModel
        {
            public Guid Id { get; set; }
        }

        public class ActivateEmployee : IWriteModel
        {
            public Guid Id { get; set; }
            public bool IsActive { get; } = true;
        }

        public class DeactivateEmployee : IWriteModel
        {
            public Guid Id { get; set; }
            public bool IsActive { get; } = false;
        }

        public class CreateEmployeeAddressInfo : IWriteModel
        {
            public Guid EmployeeId { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string StateCode { get; set; }
            public string Zipcode { get; set; }
        }

        public class EditEmployeeAddressInfo : IWriteModel
        {
            public int AddressId { get; set; }
            public Guid EmployeeId { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string StateCode { get; set; }
            public string Zipcode { get; set; }
        }

        public class DeleteEmployeeAddressInfo : IWriteModel
        {
            public int AddressId { get; set; }
            public Guid EmployeeId { get; set; }
        }

        public class CreateEmployeeContactInfo : IWriteModel
        {
            public Guid EmployeeId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleInitial { get; set; }
            public string Telephone { get; set; }
            public string Notes { get; set; }
        }

        public class EditEmployeeContactInfo : IWriteModel
        {
            public int PersonId { get; set; }
            public Guid EmployeeId { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string MiddleInitial { get; set; }
            public string Telephone { get; set; }
            public string Notes { get; set; }
        }

        public class DeleteEmployeeContactInfo : IWriteModel
        {
            public int PersonId { get; set; }
            public Guid EmployeeId { get; set; }
        }
    }
}