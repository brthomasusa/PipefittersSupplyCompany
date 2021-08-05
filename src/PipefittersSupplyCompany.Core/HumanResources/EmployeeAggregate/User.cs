using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate
{
    public class User : BaseEntity<Guid>
    {
        private readonly ICollection<Role> _roles = new List<Role>();
        private readonly ICollection<UserRole> _userRole = new List<UserRole>();
        private string _userName;
        private string _email;

        protected User() { }

        public User(Guid id, string userName, string email, Employee employee)
        {
            Id = id;
            UserName = userName;
            Email = email;
            Employee = employee;
        }

        public string UserName
        {
            get
            { return _userName; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The user name is required.", nameof(value));
                }

                if (value.Length > 30)
                {
                    throw new ArgumentException("The user name has a maximum length of 256 characters.", nameof(value));
                }

                _userName = value;
            }
        }

        public string Email
        {
            get
            { return _email; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("An email is required.", nameof(value));
                }

                if (value.Length > 30)
                {
                    throw new ArgumentException("The email has a maximum length of 256 characters.", nameof(value));
                }

                _email = value;
            }
        }

        public Employee Employee { get; private set; }

        public virtual IReadOnlyList<UserRole> RoleLink => _userRole.ToList();
    }
}