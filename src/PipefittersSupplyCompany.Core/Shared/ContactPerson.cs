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

        public ContactPerson(ExternalAgent agent, PersonName name, Telephone telephone, string notes)
            : this()
        {
            if (agent == null)
            {
                throw new ArgumentNullException("External agent is required.");
            }

            Agent = agent;
            ContactName = name;
            Telephone = telephone;
            Notes = notes;
        }

        public virtual PersonName ContactName { get; private set; }

        public virtual Telephone Telephone { get; private set; }

        public string Notes
        {
            get
            { return _notes; }

            private set
            {
                _notes = value;
            }
        }

        public virtual ExternalAgent Agent { get; private set; }
    }
}