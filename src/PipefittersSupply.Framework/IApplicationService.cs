using System.Threading.Tasks;

namespace PipefittersSupply.Framework
{
    public interface IApplicationService
    {
        Task Handle(object command);
    }
}