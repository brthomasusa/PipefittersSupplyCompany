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
            IsActive isActive
        ) =>
        Apply(new Events.EmployeeCreated
        {
            Id = id,
            EmployeeTypeId = employeeTypeId,
            LastName = lname,
            FirstName = fname,
            MiddleInitial = mi,
            SSN = ssn,
            AddressLine1 = line1,
            AddressLine2 = line2,
            City = city,
            StateProvinceCode = stateProvince,
            Zipcode = zipcode,
            Telephone = telephone,
            MaritalStatus = maritalStatus,
            Exemptions = exemptions,
            PayRate = payRate,
            StartDate = startDate,
            IsActive = isActive,
            CreatedDate = DateTime.Now
        });


        public EmployeeId Id { get; private set; }

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

        public City City { get; private set; }

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
            var valid = Id != null && EmployeeTypeId != null;

            if (!valid)
            {
                throw new InvalidEntityStateException(this, "Post-checks failed!");
            }
        }

        protected override void When(object @event)
        {
            switch (@event)
            {
                case Events.EmployeeCreated evt:
                    Id = new EmployeeId(evt.Id);
                    EmployeeTypeId = new EmployeeTypeIdentifier(evt.EmployeeTypeId);
                    LastName = new EmployeeLastName(evt.LastName);
                    FirstName = new EmployeeFirstName(evt.FirstName);
                    MiddleInitial = new EmployeeMiddleInitial(evt.MiddleInitial);
                    SSN = new EmployeeSSN(evt.SSN);
                    AddressLine1 = new AddressLine1(evt.AddressLine1);
                    AddressLine2 = new AddressLine2(evt.AddressLine2);
                    City = new City(evt.City);
                    State = new StateProvinceCode(evt.StateProvinceCode);
                    Zipcode = new Zipcode(evt.Zipcode);
                    Telephone = new Telephone(evt.Telephone);
                    MaritalStatus = new MaritalStatus(evt.MaritalStatus);
                    Exemptions = new TaxExemption(evt.Exemptions);
                    PayRate = new EmployeePayRate(evt.PayRate);
                    StartDate = new EmployeeStartDate(evt.StartDate);
                    IsActive = new IsActive(evt.IsActive);
                    CreatedDate = new CreatedDate(evt.CreatedDate);
                    break;
            }
        }
    }
}