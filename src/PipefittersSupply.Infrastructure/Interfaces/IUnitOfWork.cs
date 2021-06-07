using System.Threading.Tasks;

namespace PipefittersSupply.Infrastructure.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}