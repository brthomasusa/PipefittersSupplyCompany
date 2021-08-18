using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class ExternalAgent : AggregateRoot<Guid>
    {
        private AgentType _agentType;
        private readonly List<Address> _addresses = new List<Address>();
        private readonly List<ContactPerson> _contactPersons = new List<ContactPerson>();

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

        public virtual IReadOnlyList<Address> Addresses => _addresses.ToList();

        internal void AddAddress(AddressVO address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("Can not add null to list of agent addresses.");
            }

            _addresses.Add(new Address(this, address));
        }

        public virtual IReadOnlyList<ContactPerson> ContactPersons => _contactPersons.ToList();

        internal void AddContactPerson(PersonName name, PhoneNumber telephone, string notes)
        {
            if (name == null)
            {
                throw new ArgumentNullException("The contact person name is required.");
            }

            if (telephone == null)
            {
                throw new ArgumentNullException("The contact person telephone number is required.");
            }

            _contactPersons.Add(new ContactPerson(this, name, telephone, notes));
        }
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