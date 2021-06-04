using System;

namespace PipefittersSupply.Infrastructure.Queries.HumanResources
{
    public static class ReadModels
    {
        public class EmployeeDetails
        {
            public int Id { get; set; }
            public int EmployeeType { get; set; }
            public int Supervisor { get; set; }
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
            public DateTime LastModifiedDate { get; set; }
        }

        public class EmployeeListItems
        {
            public int Id { get; set; }
            public int EmployeeType { get; set; }
            public int Supervisor { get; set; }
            public string LastName { get; set; }
            public string FirstName { get; set; }
            public string MiddleInitial { get; set; }
            public string Telephone { get; set; }
            public bool IsActive { get; set; }
        }
    }
}