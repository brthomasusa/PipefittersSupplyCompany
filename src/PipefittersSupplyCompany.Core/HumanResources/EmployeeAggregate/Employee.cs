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


        public void Activate()
        {
            // _isActive = true;
        }

        public void Deactivate()
        {
            // _isActive = false;
        }

        public virtual ExternalAgent ExternalAgent { get; private set; }
    }
}

