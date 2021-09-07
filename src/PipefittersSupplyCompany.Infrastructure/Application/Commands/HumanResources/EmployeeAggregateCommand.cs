using System;
using PipefittersSupplyCompany.Core.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Application.Commands.HumanResources
{
    public static class EmployeeAggregateCommand
    {
        public static class V1
        {
            public class CreateEmployeeInfo : ICommand
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

            public class EditEmployeeInfo : ICommand
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

            public class ActivateEmployee : ICommand
            {
                public Guid Id { get; set; }
                public bool IsActive { get; } = true;
            }

            public class DeactivateEmployee : ICommand
            {
                public Guid Id { get; set; }
                public bool IsActive { get; } = false;
            }

            public class CreateEmployeeAddressInfo : ICommand
            {
                public Guid EmployeeId { get; set; }
                public string AddressLine1 { get; set; }
                public string AddressLine2 { get; set; }
                public string City { get; set; }
                public string StateCode { get; set; }
                public string Zipcode { get; set; }
            }
        }
    }
}