using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;

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

        public virtual Financier Financier { get; private set; }

        public virtual IReadOnlyList<Address> Addresses => _addresses.ToList();

        internal void AddAddress(int addressId, AddressVO address)
        {
            if (address == null)
            {
                throw new ArgumentNullException("Can not add null to list of agent addresses.");
            }

            var duplicate =
                (from items in Addresses
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

            _addresses.Add(new Address(addressId, this, address));
        }

        internal void UpdateAddress(int addressId, AddressVO address)
        {
            Address found = ((List<Address>)Addresses).Find(x => x.Id.Equals(addressId));

            if (found == null)
            {
                throw new ArgumentException("Unable to update address; address not found.");
            }

            if (found.AddressDetails != address)
            {
                found.UpdateAddressDetails(address);
            }
        }

        internal void DeleteAddress(int addressId)
        {
            Address found = ((List<Address>)Addresses).Find(x => x.Id.Equals(addressId));

            if (found == null)
            {
                throw new ArgumentException("Unable to delete address; address not found.");
            }

            _addresses.Remove(found);
        }

        public virtual IReadOnlyList<ContactPerson> ContactPersons => _contactPersons.ToList();

        internal void AddContactPerson(int personId, PersonName name, PhoneNumber telephone, string notes)
        {
            if (name == null)
            {
                throw new ArgumentNullException("The contact person name is required.");
            }

            if (telephone == null)
            {
                throw new ArgumentNullException("The contact person telephone number is required.");
            }

            var duplicate = ((List<ContactPerson>)ContactPersons).Exists
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

            _contactPersons.Add(new ContactPerson(personId, this, name, telephone, notes));
        }

        internal void UpdateContactPerson(int personId, PersonName name, PhoneNumber telephone, string notes)
        {
            var found = ((List<ContactPerson>)ContactPersons).Find(x => x.Id.Equals(personId));

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

        internal void DeleteContactPerson(int personId)
        {
            var found = ((List<ContactPerson>)ContactPersons).Find(x => x.Id.Equals(personId));

            if (found == null)
            {
                throw new ArgumentException("Unable to delete contact person; contact paerson not found.");
            }

            _contactPersons.Remove(found);
        }
    }
}