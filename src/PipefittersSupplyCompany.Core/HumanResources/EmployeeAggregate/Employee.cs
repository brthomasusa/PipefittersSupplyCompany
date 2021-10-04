using System;
using System.Collections.Generic;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class Employee : AggregateRoot<Guid>, IAggregateRoot
    {
        protected Employee() { }

        public Employee(ExternalAgent agent, SupervisorId supervisorId, PersonName name, SSN ssn, PhoneNumber telephone,
                        MaritalStatus maritalStatus, TaxExemption exemption, PayRate payRate, StartDate startDate, IsActive isActive)
           : this()
        {
            ExternalAgent = agent ?? throw new ArgumentNullException("The external agent is required.");
            Id = agent.Id;
            SupervisorId = supervisorId ?? throw new ArgumentNullException("The supervisor id paramater is required.");
            EmployeeName = name ?? throw new ArgumentNullException("The employee name parameter is required.");
            SSN = ssn ?? throw new ArgumentNullException("The SSN parameter is required.");
            Telephone = telephone ?? throw new ArgumentNullException("The telephone parameter is required.");
            MaritalStatus = maritalStatus ?? throw new ArgumentNullException("The marital status parameter is required.");
            TaxExemption = exemption ?? throw new ArgumentNullException("The tax exemption parameter is required.");
            PayRate = payRate ?? throw new ArgumentNullException("The pay rate parameter is required.");
            StartDate = startDate ?? throw new ArgumentNullException("The start date parameter is required.");
            IsActive = isActive ?? throw new ArgumentNullException("The is active parameter is required.");

            CheckValidity();
        }

        public virtual SupervisorId SupervisorId { get; private set; }
        public virtual PersonName EmployeeName { get; private set; }
        public virtual SSN SSN { get; private set; }
        public virtual PhoneNumber Telephone { get; private set; }
        public virtual MaritalStatus MaritalStatus { get; private set; }
        public virtual TaxExemption TaxExemption { get; private set; }
        public virtual PayRate PayRate { get; private set; }
        public virtual StartDate StartDate { get; private set; }
        public virtual IsActive IsActive { get; private set; }
        public virtual ExternalAgent ExternalAgent { get; private set; }

        protected override void CheckValidity()
        {
            if (ExternalAgent.AgentType != AgentType.Employee)
            {
                throw new InvalidOperationException("Invalid external agent type, it should be 'AgentType.Employee'.");
            }
        }

        public void UpdateSupervisorId(SupervisorId value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("The supervisor id can not be null.");
            }

            SupervisorId = value;
            CheckValidity();
        }

        public void UpdateEmployeeName(PersonName value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Employee name can not be updated with null.");
            }

            EmployeeName = value;
            CheckValidity();
        }

        public void UpdateSSN(SSN value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Employee SSN can not be updated with null.");
            }

            SSN = value;
            CheckValidity();
        }

        public void UpdateTelephone(PhoneNumber value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Employee telephone can not be updated with null.");
            }

            Telephone = value;
            CheckValidity();
        }

        public void UpdateMaritalStatus(MaritalStatus value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Employee marital status can not be updated with null.");
            }

            MaritalStatus = value;
            CheckValidity();
        }

        public void UpdateTaxExemptions(TaxExemption value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Employee tax exemptions can not be updated with null.");
            }

            TaxExemption = value;
            CheckValidity();
        }

        public void UpdatePayRate(PayRate value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Employee pay rate can not be updated with null.");
            }

            PayRate = value;
            CheckValidity();
        }

        public void UpdateStartDate(StartDate value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Employee start date can not be updated with null.");
            }

            StartDate = value;
            CheckValidity();
        }

        public void Activate() => IsActive = IsActive.Create(true);

        public void Deactivate() => IsActive = IsActive.Create(false);

        public IReadOnlyList<Address> Addresses() => ExternalAgent.Addresses;

        public void AddAddress(int addressId, AddressVO address) => ExternalAgent.AddAddress(addressId, address);

        public void UpdateAddress(int addressId, AddressVO address) => ExternalAgent.UpdateAddress(addressId, address);

        public void DeleteAddress(int addressId) => ExternalAgent.DeleteAddress(addressId);

        public IReadOnlyList<ContactPerson> ContactPersons() => ExternalAgent.ContactPersons;

        public void AddContactPerson(int personId, PersonName name, PhoneNumber telephone, string notes)
            => ExternalAgent.AddContactPerson(personId, name, telephone, notes);

        public void UpdateContactPerson(int personId, PersonName name, PhoneNumber telephone, string notes)
            => ExternalAgent.UpdateContactPerson(personId, name, telephone, notes);

        public void DeleteContactPerson(int personId) => ExternalAgent.DeleteContactPerson(personId);
    }
}