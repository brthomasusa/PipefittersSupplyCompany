using System;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.Shared
{
    public class DomainUser : BaseEntity<Guid>
    {

        private string _userName;
        private string _firstName;
        private string _lastName;
        private string _middleInitial;
        private string _email;

        protected DomainUser() { }

        public DomainUser(Guid id, string fName, string lname, string mi)
        {
            Id = id;
            FirstName = fName;
            LastName = lname;
            MiddleInitial = mi;
        }

        public string FirstName
        {
            get
            { return _firstName; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The user first name is required.", nameof(value));
                }

                if (value.Length > 25)
                {
                    throw new ArgumentException("The user first name has a maximum length of 25 characters.", nameof(value));
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
                    throw new ArgumentNullException("The user last name is required.", nameof(value));
                }

                if (value.Length > 25)
                {
                    throw new ArgumentException("The user last name has a maximum length of 25 characters.", nameof(value));
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
                if (!string.IsNullOrEmpty(value))
                {
                    value = value.Trim();
                    if (value.Length > 1)
                    {
                        throw new ArgumentOutOfRangeException("Maximum length of middle initial is 1 character.", nameof(value));
                    }
                }

                _middleInitial = value;
            }
        }
    }
}