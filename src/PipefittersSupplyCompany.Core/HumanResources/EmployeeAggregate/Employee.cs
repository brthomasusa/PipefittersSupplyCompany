using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate.Events;
using PipefittersSupplyCompany.Core.Exceptions;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class Employee : AggregateRoot<Guid>
    {
        protected Employee() { }

        public Employee(ExternalAgent agent, SupervisorId supervisorId, PersonName name, SSN ssn, PhoneNumber telephone,
                        MaritalStatus maritalStatus, TaxExemption exemption, PayRate payRate, StartDate startDate, IsActive isActive)
           : this()
        {
            if (agent == null)
            {
                throw new ArgumentNullException("The external agent is required.");
            }

            Id = agent.Id;
            ExternalAgent = agent;
            SupervisorId = supervisorId;
            EmployeeName = name;
            SSN = ssn;
            Telephone = telephone;
            MaritalStatus = maritalStatus;
            TaxExemption = exemption;
            PayRate = payRate;
            StartDate = startDate;
            IsActive = isActive;

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

        public IReadOnlyList<Address> Addresses() => ExternalAgent.Addresses;

        public void AddAddress(int id, AddressVO address) => ExternalAgent.AddAddress(id, address);

        public IReadOnlyList<ContactPerson> ContactPersons() => ExternalAgent.ContactPersons;

        public void AddContactPerson(int id, PersonName name, PhoneNumber telephone, string notes)
            => ExternalAgent.AddContactPerson(id, name, telephone, notes);

        public void Activate() => IsActive = IsActive.Create(true);

        public void Deactivate() => IsActive = IsActive.Create(false);

        public virtual ExternalAgent ExternalAgent { get; private set; }
    }
}

