using System;

namespace PipefittersSupply.Contracts.HumanResources
{
    public static class EmployeeCommand
    {
        public static class V1
        {
            public class Create
            {
                public int Id { get; set; }
                public int EmployeeTypeId { get; set; }
                public int SupervisorId { get; set; }
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
            }

            public class UpdateEmployeeTypeId
            {
                public int Id { get; set; }
                public int EmployeeTypeId { get; set; }
            }

            public class UpdateSupervisorId
            {
                public int Id { get; set; }
                public int SupervisorId { get; set; }
            }

            public class UpdateEmployeeLastName
            {
                public int Id { get; set; }
                public string LastName { get; set; }
            }

            public class UpdateEmployeeFirstName
            {
                public int Id { get; set; }
                public string FirstName { get; set; }
            }

            public class UpdateEmployeeMiddleInitial
            {
                public int Id { get; set; }
                public string MiddleInitial { get; set; }
            }

            public class UpdateEmployeeSSN
            {
                public int Id { get; set; }
                public string SSN { get; set; }
            }

            public class UpdateAddressLine1
            {
                public int Id { get; set; }
                public string AddressLine1 { get; set; }
            }

            public class UpdateAddressLine2
            {
                public int Id { get; set; }
                public string AddressLine2 { get; set; }
            }

            public class UpdateCity
            {
                public int Id { get; set; }
                public string City { get; set; }
            }

            public class UpdateStateProvinceCode
            {
                public int Id { get; set; }
                public string StateProvinceCode { get; set; }
            }

            public class UpdateZipcode
            {
                public int Id { get; set; }
                public string Zipcode { get; set; }
            }

            public class UpdateTelephone
            {
                public int Id { get; set; }
                public string Telephone { get; set; }
            }

            public class UpdateMaritalStatus
            {
                public int Id { get; set; }
                public string MaritalStatus { get; set; }
            }

            public class UpdateTaxExemption
            {
                public int Id { get; set; }
                public int Exemptions { get; set; }
            }

            public class UpdateEmployeePayRate
            {
                public int Id { get; set; }
                public decimal PayRate { get; set; }
            }

            public class UpdateEmployeeStartDate
            {
                public int Id { get; set; }
                public DateTime StartDate { get; set; }
            }

            public class UpdateIsActive
            {
                public int Id { get; set; }
                public bool IsActive { get; set; }
            }
        }
    }
}