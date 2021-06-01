using System.Threading.Tasks;
using PipefittersSupply.Domain.HumanResources.Employees;

namespace PipefittersSupply.Domain.Repository
{
    public interface IEmployeeTypeRepository
    {
        Task Add(EmployeeType entity);
        Task<bool> Exists(EmployeeTypeIdentifier id);
        Task<EmployeeType> Load(EmployeeTypeIdentifier id);
    }
}