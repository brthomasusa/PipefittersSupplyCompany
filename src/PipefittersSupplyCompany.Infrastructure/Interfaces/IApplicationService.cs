using System.Threading.Tasks;
using PipefittersSupplyCompany.SharedKernel.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IApplicationService
    {
        Task Handle(ICommand command);
    }
}