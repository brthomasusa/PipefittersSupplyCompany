using System;
using System.Text.RegularExpressions;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class ContactPerson : Entity<int>
    {
        private string _firstName;
        private string _lastName;
        private string _middleInitial;
        private string _telephone;
        private string _notes;

        protected ContactPerson() { }

        public ContactPerson(ExternalAgent agent, string lastName, string firstName, string mi, string telephone, string notes)
            : this()
        {
            if (agent == null)
            {
                throw new ArgumentNullException("External agent is required.");
            }

            if (string.IsNullOrEmpty(lastName))
            {
                throw new ArgumentNullException("The last name of the contact person is required.");
            }

            if (string.IsNullOrEmpty(firstName))
            {
                throw new ArgumentNullException("The first name of the contact person is required.");
            }

            if (string.IsNullOrEmpty(telephone))
            {
                throw new ArgumentNullException("The telephone number of the contact person is required.");
            }

            Agent = agent;
            FirstName = firstName;
            LastName = lastName;
            MiddleInitial = mi;
            Telephone = telephone;
            Notes = notes;
        }

        public string FirstName
        {
            get
            { return _firstName; }

            private set
            {
                if (value.Length > 25)
                {
                    throw new ArgumentException("The employee first name maximum length is 25 characters.", nameof(value));
                }

                _firstName = value;
            }
        }

        public string LastName
        {
            get
            { return _lastName; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The employee last name is required.");
                }

                if (value.Length > 25)
                {
                    throw new ArgumentException("The employee last name maximum length is 25 characters.");
                }

                _lastName = value;
            }
        }

        public string MiddleInitial
        {
            get
            { return _middleInitial; }

            private set
            {
                if (!string.IsNullOrEmpty(value) && value.Length > 1)
                {
                    throw new ArgumentException("The middle initial maximum length is 1 character.");
                }

                _middleInitial = value;
            }
        }

        public string Telephone
        {
            get
            { return _telephone; }

            private set
            {
                string rgTelephone = @"^(\+\d{1,2}\s)?\(?\d{3}\)?[\s.-]?\d{3}[\s.-]?\d{4}$";

                if (!Regex.IsMatch(value, rgTelephone))
                {
                    throw new ArgumentException("Invalid telephone number!");
                }

                _telephone = value;
            }
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

        public virtual ExternalAgent Agent { get; private set; }
    }
}