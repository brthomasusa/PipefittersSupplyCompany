using System;
using System.Collections.Generic;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.Interfaces;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;

namespace PipefittersSupplyCompany.Core.Financing.FinancierAggregate
{
    public class Financier : AggregateRoot<Guid>, IAggregateRoot
    {
        private List<LoanAgreement> _loanAgreements = new List<LoanAgreement>();


        protected Financier() { }

        public Financier(ExternalAgent agent, OrganizationName name, PhoneNumber telephone, IsActive isActive, Guid userID)
        {
            ExternalAgent = agent ?? throw new ArgumentNullException("The external agent is required.");

            Id = agent.Id;
            FinancierName = name ?? throw new ArgumentNullException("The financier name parameter is required.");
            Telephone = telephone ?? throw new ArgumentNullException("The telephone parameter is required.");
            IsActive = isActive ?? throw new ArgumentNullException("The is active parameter is required.");

            if (userID == default)
            {
                throw new ArgumentNullException("The user id (employee creating this record) parameter is required.");
            }
            UserId = userID;
        }

        public virtual ExternalAgent ExternalAgent { get; private set; }

        public virtual OrganizationName FinancierName { get; private set; }

        public virtual PhoneNumber Telephone { get; private set; }

        public virtual IsActive IsActive { get; private set; }

        public Guid UserId { get; private set; }

        public virtual IReadOnlyList<LoanAgreement> LoanAgreements => _loanAgreements;

        public void UpdateFinancierName(OrganizationName value)
        {
            FinancierName = value ?? throw new ArgumentNullException("The financier name parameter is required.");
            CheckValidity();
        }


        public void UpdateTelephone(PhoneNumber value)
        {
            Telephone = value ?? throw new ArgumentNullException("The telephone parameter is required.");
            CheckValidity();
        }

        public void Activate() => IsActive = IsActive.Create(true);

        public void Deactivate() => IsActive = IsActive.Create(false);

        public void UpdateUserId(Guid value)
        {
            if (value == default)
            {
                throw new ArgumentNullException("User id can not be updated with default Guid value.");
            }

            UserId = value;
            CheckValidity();
        }

        protected override void CheckValidity()
        {
            if (ExternalAgent.AgentType is not AgentType.Financier)
            {
                throw new ArgumentException("Invalid ExternalAgent type; it must be 'AgentType.Financier'.");
            }
        }

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