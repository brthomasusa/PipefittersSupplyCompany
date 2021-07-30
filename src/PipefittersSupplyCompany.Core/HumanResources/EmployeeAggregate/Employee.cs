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

        public Employee(Guid id, EmployeeType employeeType, Employee supervisor, PersonName name,
                        Address address, SSN ssn, Telephone telephone, MaritalStatus maritalStatus,
                        TaxExemption exemption, PayRate payRate, StartDate startDate, IsActive isActive)
            : this()
        {
            Id = id;
            EmployeeType = employeeType;
            Supervisor = supervisor;
            Name = name;
            Address = address;
            SSN = ssn;
            Telephone = telephone;
            MaritalStatus = maritalStatus;
            TaxExemption = exemption;
            PayRate = payRate;
            StartDate = startDate;
            IsActive = isActive;
        }

        public virtual EmployeeType EmployeeType { get; private set; }

        public virtual Employee Supervisor { get; private set; }

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

