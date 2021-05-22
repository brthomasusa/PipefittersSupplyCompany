using System.Threading.Tasks;
using PipefittersSupply.Domain.HumanResources.Employees;

namespace PipefittersSupply.Domain.Repository
{
    public interface IEmployeeRepository
    {
        Task<bool> Exists(EmployeeId id);
        Task<Employee> Load(EmployeeId id);
        Task Save(Employee entity);
    }
}