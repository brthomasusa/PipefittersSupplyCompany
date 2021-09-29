using System.Threading.Tasks;
using PipefittersSupplyCompany.Core.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface ICommandHandler
    {
        // ICommandHandler NextHandler { get; set; }
        Task Handle(IWriteModel writeModel);
    }
}