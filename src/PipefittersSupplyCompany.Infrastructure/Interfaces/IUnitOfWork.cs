using System.Threading.Tasks;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}