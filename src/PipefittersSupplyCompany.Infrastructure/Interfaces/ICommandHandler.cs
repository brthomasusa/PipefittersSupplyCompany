using System.Threading.Tasks;
using PipefittersSupplyCompany.Infrastructure.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface ICommandHandler
    {
        Task Handle(IWriteModel writeModel);
    }
}