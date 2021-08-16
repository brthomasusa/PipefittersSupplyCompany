using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;

namespace PipefittersSupplyCompany.Core.HumanResources
{
    public class UserRole : BaseEntity<int>
    {
        protected UserRole() { }

        // public UserRole(int id, User user, Role role)
        // {
        //     Id = id;
        //     User = user;
        //     Role = role;
        // }

        // public virtual User User { get; private set; }

        // public virtual Role Role { get; private set; }
    }
}