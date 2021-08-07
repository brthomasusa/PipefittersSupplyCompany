using System;
using System.Collections.Generic;
using System.Linq;
using PipefittersSupplyCompany.SharedKernel;

namespace PipefittersSupplyCompany.Core.HumanResources
{
    public class Role : BaseEntity<Guid>
    {
        private readonly ICollection<UserRole> _userRole = new List<UserRole>();
        private string _roleName;

        protected Role() { }

        public Role(Guid id, string roleName)
        {
            Id = id;
            RoleName = roleName;
        }

        public string RoleName
        {
            get
            { return _roleName; }

            private set
            {
                if (string.IsNullOrEmpty(value))
                {
                    throw new ArgumentNullException("The role name is required.", nameof(value));
                }

                if (value.Length > 30)
                {
                    throw new ArgumentException("The role name has a maximum length of 256 characters.", nameof(value));
                }

                _roleName = value;
            }
        }

        public virtual IReadOnlyList<UserRole> UserLink => _userRole.ToList();
    }
}