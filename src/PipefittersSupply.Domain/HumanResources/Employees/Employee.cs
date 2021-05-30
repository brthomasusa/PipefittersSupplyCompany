using System;
using PipefittersSupply.Domain.Common;
using PipefittersSupply.Domain.Base;

namespace PipefittersSupply.Domain.HumanResources.Employees
{
    public class Employee : AggregateRoot<EmployeeId>
    {
        public Employee(
            EmployeeId id,
            EmployeeTypeIdentifier employeeTypeId,
            EmployeeId supervisorID,
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
                SupervisorId = supervisorID,
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

        public EmployeeTypeIdentifier EmployeeTypeId { get; private set; }
        public void UpdateEmployeeTypeId(EmployeeTypeIdentifier employeeTypeId) =>
            Apply(new Events.EmployeeTypeIdUpdated
            {
                Id = Id,
                EmployeeTypeId = employeeTypeId,
                LastModifiedDate = DateTime.Now
            });

        public EmployeeId SupervisorId { get; private set; }
        public void UpdateSupervisorId(EmployeeId value) =>
            Apply(new Events.SupervisorIdUpdated
            {
                Id = Id,
                SupervisorId = value,
                LastModifiedDate = DateTime.Now
            });

        public EmployeeLastName LastName { get; private set; }
        public void UpdateLastName(EmployeeLastName lname) =>
            Apply(new Events.EmployeeLastNameUpdated
            {
                Id = Id,
                LastName = lname,
                LastModifiedDate = DateTime.Now
            });

        public EmployeeFirstName FirstName { get; private set; }
        public void UpdateFirstName(EmployeeFirstName fname) =>
            Apply(new Events.EmployeeFirstNameUpdated
            {
                Id = Id,
                FirstName = fname,
                LastModifiedDate = DateTime.Now
            });

        public EmployeeMiddleInitial MiddleInitial { get; private set; }
        public void UpdateMiddleInitial(EmployeeMiddleInitial mi) =>
            Apply(new Events.EmployeeMiddleInitialUpdated
            {
                Id = Id,
                MiddleInitial = mi,
                LastModifiedDate = DateTime.Now
            });

        public EmployeeSSN SSN { get; private set; }
        public void UpdateSSN(EmployeeSSN ssn) =>
            Apply(new Events.EmployeeSSNUpdated
            {
                Id = Id,
                SSN = ssn,
                LastModifiedDate = DateTime.Now
            });

        public AddressLine1 AddressLine1 { get; private set; }
        public void UpdateAddressLine1(AddressLine1 address) =>
            Apply(new Events.AddressLine1Updated
            {
                Id = Id,
                AddressLine1 = address,
                LastModifiedDate = DateTime.Now
            });

        public AddressLine2 AddressLine2 { get; private set; }
        public void UpdateAddressLine2(AddressLine2 address) =>
            Apply(new Events.AddressLine2Updated
            {
                Id = Id,
                AddressLine2 = address,
                LastModifiedDate = DateTime.Now
            });

        public City City { get; private set; }
        public void UpdateCity(City city) =>
            Apply(new Events.CityUpdated
            {
                Id = Id,
                City = city,
                LastModifiedDate = DateTime.Now
            });

        public StateProvinceCode State { get; private set; }
        public void UpdateStateProvinceCode(StateProvinceCode stateCode) =>
            Apply(new Events.StateProvinceCodeUpdated
            {
                Id = Id,
                StateProvinceCode = stateCode,
                LastModifiedDate = DateTime.Now
            });

        public Zipcode Zipcode { get; private set; }
        public void UpdateZipcode(Zipcode zipcode) =>
            Apply(new Events.ZipcodeUpdated
            {
                Id = Id,
                Zipcode = zipcode,
                LastModifiedDate = DateTime.Now
            });

        public Telephone Telephone { get; private set; }
        public void UpdateTelephone(Telephone phone) =>
            Apply(new Events.TelephoneUpdated
            {
                Id = Id,
                Telephone = phone,
                LastModifiedDate = DateTime.Now
            });

        public MaritalStatus MaritalStatus { get; private set; }
        public void UpdateMaritalStatus(MaritalStatus status) =>
            Apply(new Events.MaritalStatusUpdated
            {
                Id = Id,
                MaritalStatus = status,
                LastModifiedDate = DateTime.Now
            });

        public TaxExemption Exemptions { get; private set; }
        public void UpdateTaxExemption(TaxExemption taxExemptions) =>
            Apply(new Events.TaxExemptionUpdated
            {
                Id = Id,
                Exemptions = taxExemptions,
                LastModifiedDate = DateTime.Now
            });

        public EmployeePayRate PayRate { get; private set; }
        public void UpdatePayRate(EmployeePayRate rate) =>
            Apply(new Events.EmployeePayRateUpdated
            {
                Id = Id,
                PayRate = rate,
                LastModifiedDate = DateTime.Now
            });

        public EmployeeStartDate StartDate { get; private set; }
        public void UpdateStartDate(EmployeeStartDate startDate) =>
            Apply(new Events.EmployeeStartDateUpdated
            {
                Id = Id,
                StartDate = startDate,
                LastModifiedDate = DateTime.Now
            });

        public IsActive IsActive { get; private set; }
        public void UpdateIsActive(IsActive status) =>
            Apply(new Events.IsActiveUpdated
            {
                Id = Id,
                IsActive = status,
                LastModifiedDate = DateTime.Now
            });

        public CreatedDate CreatedDate { get; private set; }

        public LastModifiedDate LastModifiedDate { get; private set; }

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
                    SupervisorId = new EmployeeId(evt.SupervisorId);
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
                case Events.EmployeeTypeIdUpdated evt:
                    EmployeeTypeId = new EmployeeTypeIdentifier(evt.EmployeeTypeId);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.SupervisorIdUpdated evt:
                    SupervisorId = new EmployeeId(evt.SupervisorId);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.EmployeeLastNameUpdated evt:
                    LastName = new EmployeeLastName(evt.LastName);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.EmployeeFirstNameUpdated evt:
                    FirstName = new EmployeeFirstName(evt.FirstName);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.EmployeeMiddleInitialUpdated evt:
                    MiddleInitial = new EmployeeMiddleInitial(evt.MiddleInitial);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.EmployeeSSNUpdated evt:
                    SSN = new EmployeeSSN(evt.SSN);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.AddressLine1Updated evt:
                    AddressLine1 = new AddressLine1(evt.AddressLine1);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.AddressLine2Updated evt:
                    AddressLine2 = new AddressLine2(evt.AddressLine2);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.CityUpdated evt:
                    City = new City(evt.City);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.StateProvinceCodeUpdated evt:
                    State = new StateProvinceCode(evt.StateProvinceCode);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.ZipcodeUpdated evt:
                    Zipcode = new Zipcode(evt.Zipcode);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.TelephoneUpdated evt:
                    Telephone = new Telephone(evt.Telephone);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.MaritalStatusUpdated evt:
                    MaritalStatus = new MaritalStatus(evt.MaritalStatus);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.TaxExemptionUpdated evt:
                    Exemptions = new TaxExemption(evt.Exemptions);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.EmployeePayRateUpdated evt:
                    PayRate = new EmployeePayRate(evt.PayRate);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.EmployeeStartDateUpdated evt:
                    StartDate = new EmployeeStartDate(evt.StartDate);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
                case Events.IsActiveUpdated evt:
                    IsActive = new IsActive(evt.IsActive);
                    LastModifiedDate = new LastModifiedDate(DateTime.Now);
                    break;
            }
        }
    }
}
