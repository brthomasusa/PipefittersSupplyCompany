using System;
using System.Collections.Generic;
using System.Linq;
using Ardalis.GuardClauses;
// using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate.Events;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class Employee : BaseEntity<Guid>, IAggregateRoot
    {
        protected Employee() { }

        public Employee(Guid id, EmployeeType employeeType, Guid supervisorId, PersonName name,
                        Address address, SSN ssn, Telephone telephone, MaritalStatus maritalStatus,
                        TaxExemption exemption, PayRate payRate, StartDate startDate, IsActive isActive)
            : this()
        {
            if (id == default)
            {
                throw new ArgumentNullException("The employee id is required.", nameof(id));
            }
            Id = id;

            EmployeeType = Guard.Against.Null(employeeType, nameof(employeeType));

            if (supervisorId == default)
            {
                throw new ArgumentNullException("The supervisor id is required.", nameof(supervisorId));
            }
            SupervisorId = supervisorId;

            Name = Guard.Against.Null(name, nameof(name));
            Address = Guard.Against.Null(address, nameof(address));
            SSN = Guard.Against.Null(ssn, nameof(ssn));
            Telephone = Guard.Against.Null(telephone, nameof(telephone));
            MaritalStatus = Guard.Against.Null(maritalStatus, nameof(maritalStatus));
            TaxExemption = Guard.Against.Null(exemption, nameof(exemption));
            PayRate = Guard.Against.Null(payRate, nameof(payRate));
            StartDate = Guard.Against.Null(startDate, nameof(startDate));
            IsActive = Guard.Against.Null(isActive, nameof(isActive));
        }

        public virtual EmployeeType EmployeeType { get; private set; }

        public virtual Guid SupervisorId { get; private set; }

        public virtual PersonName Name { get; private set; }

        public virtual Address Address { get; private set; }

        public virtual SSN SSN { get; private set; }

        public virtual Telephone Telephone { get; private set; }

        public virtual MaritalStatus MaritalStatus { get; private set; }

        public virtual TaxExemption TaxExemption { get; private set; }

        public virtual PayRate PayRate { get; private set; }

        public virtual StartDate StartDate { get; private set; }

        public virtual IsActive IsActive { get; private set; }

        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public void Activate() { }

        public void Deactivate() { }
    }
}

