using System.Threading.Tasks;
using PipefittersSupplyCompany.Core.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface ICommandHandler
    {
        Task Handle(ICommand command);
    }
}