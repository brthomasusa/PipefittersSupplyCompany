using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class ExternalAgent : BaseEntity<Guid>
    {
        private AgentType _agentType;
        private readonly IList<Address> _addresses = new List<Address>();
        private readonly IList<ContactPerson> _contactPersons = new List<ContactPerson>();

        protected ExternalAgent() { }

        public ExternalAgent(Guid agentId, AgentType agentType)
            : this()
        {
            if (agentId == default)
            {
                throw new ArgumentNullException("The agent id is required.");
            }
            Id = agentId;

            AgentType = agentType;
        }

        public AgentType AgentType
        {
            get { return _agentType; }

            private set
            {
                if (!Enum.IsDefined(typeof(AgentType), value))
                {
                    throw new ArgumentException("Undefined agent type.");
                }

                _agentType = value;
            }
        }

        public virtual Employee Employee { get; private set; }

        public void SetEmployee(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException("The employee to link to the external agent is required.");
            }

            if (Employee != null)
            {
                throw new InvalidOperationException("The external agent and the Employee have already been linked, they can not be re-linked.");
            }

            if (AgentType != AgentType.Employee)
            {
                throw new InvalidOperationException("Can not set employee if agent type does not equal employee.");
            }

            if (Id != employee.Id)
            {
                throw new InvalidOperationException("The external agent id and the employee id do not match.");
            }

            Employee = employee;
        }


        public virtual IReadOnlyList<Address> Addresses => _addresses.ToList();

        public virtual IReadOnlyList<ContactPerson> ContactPersons => _contactPersons.ToList();
    }


    public enum AgentType : int
    {
        Customer = 1,
        Creditor = 2,
        Stockholder = 3,
        Vendor = 4,
        Employee = 5
    }
}

// AgentTypeName = agentType switch
// {
//     AgentType.Customer => "Customer",
//     AgentType.Creditor => "Creditor",
//     AgentType.Stockholder => "Stockholder",
//     AgentType.Vendor => "Vendor",
//     AgentType.Employee => "Employee",
//     _ => throw new ArgumentException("Invalid agent type, should be 'Customer', 'Creditor', 'Stockholder', 'Vendor', or 'Employee'.", nameof(agentType)),
// };