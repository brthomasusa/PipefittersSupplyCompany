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

        internal void AddAddress(int id, AddressVO address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("Can not add null to list of agent addresses.");
            }

            var duplicate =
                (from items in _addresses
                 where
                     items.AddressDetails.AddressLine1 == address.AddressLine1 &&
                     items.AddressDetails.AddressLine2 == address.AddressLine2 &&
                     items.AddressDetails.City == address.City &&
                     items.AddressDetails.StateCode == address.StateCode &&
                     items.AddressDetails.Zipcode == address.Zipcode
                 select items).SingleOrDefault();

            if (duplicate != null)
            {
                throw new InvalidOperationException("We already have this address.");
            }

            _addresses.Add(new Address(id, this, address));
        }

        internal void UpdateAddress(int id, AddressVO address)
        {
            var found = _addresses.Find(x => x.Id.Equals(id));

            if (found == null)
            {
                throw new ArgumentException("Unable to update address; address not found.");
            }

            if (found.AddressDetails != address)
            {
                found.UpdateAddressDetails(address);
            }
        }

        internal void DeleteAddress(int id)
        {
            var found = _addresses.Find(x => x.Id.Equals(id));

            if (found == null)
            {
                throw new ArgumentException("Unable to delete address; address not found.");
            }

            _addresses.Remove(found);
        }

        public virtual IReadOnlyList<ContactPerson> ContactPersons => _contactPersons.ToList();

        internal void AddContactPerson(int id, PersonName name, PhoneNumber telephone, string notes)
        {
            if (name == null)
            {
                throw new ArgumentNullException("The contact person name is required.");
            }

            if (telephone == null)
            {
                throw new ArgumentNullException("The contact person telephone number is required.");
            }

            var duplicate = _contactPersons.Exists
            (x =>
                x.ContactName.FirstName == name.FirstName &&
                x.ContactName.LastName == name.LastName &&
                x.ContactName.MiddleInitial == name.MiddleInitial &&
                x.Telephone == telephone
            );

            if (duplicate)
            {
                throw new InvalidOperationException("We already have this contact person.");
            }

            _contactPersons.Add(new ContactPerson(id, this, name, telephone, notes));
        }

        internal void UpdateContactPerson(int id, PersonName name, PhoneNumber telephone, string notes)
        {
            var found = _contactPersons.Find(x => x.Id.Equals(id));

            if (found == null)
            {
                throw new ArgumentException("Unable to update contact person; contact paerson not found.");
            }

            if (found.ContactName != name)
            {
                found.UpdateContactName(name);
            }

            if (found.Telephone != telephone)
            {
                found.UpdateTelephone(telephone);
            }

            if (found.Notes != notes)
            {
                found.UpdateNotes(notes);
            }
        }

        internal void DeleteContactPerson(int id)
        {
            var found = _contactPersons.Find(x => x.Id.Equals(id));

            if (found == null)
            {
                throw new ArgumentException("Unable to delete contact person; contact paerson not found.");
            }

            _contactPersons.Remove(found);
        }
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