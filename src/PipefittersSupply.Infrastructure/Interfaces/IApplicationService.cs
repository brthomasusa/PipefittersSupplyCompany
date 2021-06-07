using System.Threading.Tasks;

namespace PipefittersSupply.Infrastructure.Interfaces
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}