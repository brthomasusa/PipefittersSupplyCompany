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

        public Employee(Guid id, EmployeeType employeeType, Employee supervisor, PersonName name, Address address)
            : this()
        {
            Id = id;
            EmployeeType = employeeType;
            Supervisor = supervisor;
            Name = name;
            Address = address;
        }

        public virtual EmployeeType EmployeeType { get; private set; }

        public virtual Employee Supervisor { get; private set; }

        public virtual PersonName Name { get; private set; }

        public virtual Address Address { get; private set; }

        public bool IsActive { get; private set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastModifiedDate { get; set; }

        public void Activate() { }

        public void Deactivate() { }
    }
}
// LastName = lname,
// FirstName = fname,
// MiddleInitial = mi,
// SSN = ssn,
// AddressLine1 = line1,
// AddressLine2 = line2,
// City = city,
// StateProvinceCode = stateProvince,
// Zipcode = zipcode,
// Telephone = telephone,
// MaritalStatus = maritalStatus,
// Exemptions = exemptions,
// PayRate = payRate,
// StartDate = startDate,

