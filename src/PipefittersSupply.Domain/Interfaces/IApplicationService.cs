using System.Threading.Tasks;

namespace PipefittersSupply.Domain.Interfaces
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}