using System.Threading.Tasks;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface ICommandHandler
    {
        Task Handle(ICommand command);
    }
}