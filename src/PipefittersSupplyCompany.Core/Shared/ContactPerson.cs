using System;
using System.Text.RegularExpressions;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class ContactPerson : Entity<int>
    {
        private string _notes;

        protected ContactPerson() { }

        public ContactPerson(int id, ExternalAgent agent, PersonName name, PhoneNumber telephone, string notes)
            : this()
        {
            if (agent == null)
            {
                throw new ArgumentNullException("External agent is required.");
            }

            Id = id;
            Agent = agent;
            ContactName = name;
            Telephone = telephone;
            Notes = notes;

            CheckValidity();
        }

        public virtual PersonName ContactName { get; private set; }

        public void UpdateContactName(PersonName value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Contact person name can not be updated with null.");
            }

            ContactName = value;
            CheckValidity();
        }

        public virtual PhoneNumber Telephone { get; private set; }

        public void UpdateTelephone(PhoneNumber value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("Contact person telephone can not be updated with null.");
            }

            Telephone = value;
            CheckValidity();
        }

        public string Notes
        {
            get
            { return _notes; }

            private set
            {
                _notes = value;
            }
        }

        public void UpdateNotes(string value)
        {
            Notes = value;
        }

        public virtual ExternalAgent Agent { get; private set; }
    }
}