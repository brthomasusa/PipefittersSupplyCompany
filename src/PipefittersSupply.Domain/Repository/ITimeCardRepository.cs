using System.Threading.Tasks;
using PipefittersSupply.Domain.HumanResources.TimeCards;

namespace PipefittersSupply.Domain.Repository
{
    public interface ITimeCardRepository
    {
        Task<bool> Exists(TimeCardId id);
        Task<TimeCard> Load(TimeCardId id);
        Task Save(TimeCard entity);
    }
}