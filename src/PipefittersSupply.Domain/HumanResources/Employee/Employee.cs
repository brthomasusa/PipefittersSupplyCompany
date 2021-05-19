using System;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Framework;

namespace PipefittersSupply.Domain.HumanResources.Employee
{
    public class Employee : Entity<EmployeeId>
    {
        public Employee(
            EmployeeId id,
            EmployeeTypeIdentifier employeeTypeId,
            EmployeeLastName lname,
            EmployeeFirstName fname,
            EmployeeMiddleInitial mi,
            EmployeeSSN ssn,
            AddressLine1 line1,
            AddressLine2 line2,
            City city,
            StateProvinceCode stateProvince,
            Zipcode zipcode,
            Telephone telephone,
            MaritalStatus maritalStatus,
            TaxExemption exemptions,
            EmployeePayRate payRate,
            EmployeeStartDate startDate,
            IsActive isActive,
            CreatedDate createdDate
        ) =>
        Apply(new Events.EmployeeCreated
        {
            Id = id,
            EmployeeTypeId = employeeTypeId,
            LastName = lname


            // public string FirstName { get; set; }
            // public string MiddleInitial { get; set; }
            // public string SSN { get; set; }
            // public string AddressLine1 { get; set; }
            // public string AddressLine2 { get; set; }
            // public string City { get; set; }
            // public string StateProvinceCode { get; set; }
            // public string Zipcode { get; set; }
            // public string Telephone { get; set; }
            // public string MaritalStatus { get; set; }
            // public int Exemptions { get; set; }
            // public decimal PayRate { get; set; }
            // public DateTime StartDate { get; set; }
            // public DateTime CreatedDate { get; set; }            
        });


        public EmployeeId Id { get; }

        public EmployeeTypeIdentifier EmployeeTypeId { get; private set; }

        public EmployeeLastName LastName { get; private set; }
        public void UpdateLastName(EmployeeLastName lname) { }

        public EmployeeFirstName FirstName { get; private set; }
        public void UpdateFirstName(EmployeeFirstName fname) { }

        public EmployeeMiddleInitial MiddleInitial { get; private set; }
        public void UpdateMiddleInitial(EmployeeMiddleInitial mi) { }

        public EmployeeSSN SSN { get; private set; }
        public void UpdateSSN(EmployeeSSN ssn) { }

        public AddressLine1 AddressLine1 { get; private set; }

        public AddressLine2 AddressLine2 { get; private set; }

        public City City { get; set; }

        public StateProvinceCode State { get; private set; }

        public Zipcode Zipcode { get; private set; }

        public Telephone Telephone { get; private set; }

        public MaritalStatus MaritalStatus { get; private set; }

        public TaxExemption Exemptions { get; private set; }

        public EmployeePayRate PayRate { get; private set; }

        public EmployeeStartDate StartDate { get; private set; }

        public IsActive IsActive { get; private set; }

        public CreatedDate CreatedDate { get; private set; }

        public LastModifiedDate LastModifiedDate { get; private set; }

        public void UpdateLastModifiedDate(LastModifiedDate dateModified)
        {

        }

        protected override void EnsureValidState()
        {
            throw new System.NotImplementedException();
        }

        protected override void When(object @event)
        {
            throw new System.NotImplementedException();
        }
    }
}