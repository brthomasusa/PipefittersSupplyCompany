using System.Threading.Tasks;

namespace PipefittersSupply.Domain.Interfaces
{
    public interface IUnitOfWork
    {
        Task Commit();
    }
}