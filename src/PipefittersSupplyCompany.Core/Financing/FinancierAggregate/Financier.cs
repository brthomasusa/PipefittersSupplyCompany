using System;
using System.Collections.Generic;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;

namespace PipefittersSupplyCompany.Core.Financing.FinancierAggregate
{
    public class Financier : AggregateRoot<Guid>, IAggregateRoot
    {
        protected Financier() { }

        public Financier(ExternalAgent agent, OrganizationName name, PhoneNumber telephone, IsActive isActive, User user)
        {
            ExternalAgent = agent ?? throw new ArgumentNullException("The external agent is required.");
            Id = agent.Id;
            FinancierName = name ?? throw new ArgumentNullException("The financier name parameter is required.");
            Telephone = telephone ?? throw new ArgumentNullException("The telephone parameter is required.");
            IsActive = isActive ?? throw new ArgumentNullException("The is active parameter is required.");
            User = user ?? throw new ArgumentNullException("The user (employee creating this record) parameter is required.");
        }

        public virtual ExternalAgent ExternalAgent { get; private set; }

        public virtual OrganizationName FinancierName { get; private set; }

        public virtual PhoneNumber Telephone { get; private set; }

        public virtual IsActive IsActive { get; private set; }

        public virtual User User { get; set; }

        protected override void CheckValidity()
        {
            if (ExternalAgent.AgentType != AgentType.Financier)
            {
                throw new InvalidOperationException("Invalid external agent type, it should be 'AgentType.Financier'.");
            }
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