using System;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public static class Events
    {
        public class EmployeeCreated
        {
            public int Id { get; set; }
            public int EmployeeTypeId { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public string SSN { get; set; }
            public string AddressLine1 { get; set; }
            public string AddressLine2 { get; set; }
            public string City { get; set; }
            public string StateProvinceCode { get; set; }
            public string Zipcode { get; set; }
            public string Telephone { get; set; }
            public string MaritalStatus { get; set; }
            public int Exemptions { get; set; }
            public decimal PayRate { get; set; }
            public DateTime StartDate { get; set; }
            public bool IsActive { get; set; }
            public DateTime CreatedDate { get; set; }
        }

        public class EmployeeLastNameUpdated
        {
            public int Id { get; set; }
            public string LastName { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class EmployeeFirstNameUpdated
        {
            public int Id { get; set; }
            public string FirstName { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class EmployeeMiddleInitialUpdated
        {
            public int Id { get; set; }
            public string MiddleInitial { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }

        public class EmployeeSSNUpdated
        {
            public int Id { get; set; }
            public string SSN { get; set; }
            public DateTime LastModifiedDate { get; set; }
        }
    }
}
