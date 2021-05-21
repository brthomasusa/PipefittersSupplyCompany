using System.Threading.Tasks;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.Repository;

namespace PipefittersSupply.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public Task<bool> Exists(EmployeeId id)
        {
            return null;
        }

        public Task<Employee> Load(EmployeeId id)
        {
            return null;
        }

        public Task Save(Employee entity)
        {
            return null;
        }
    }
}