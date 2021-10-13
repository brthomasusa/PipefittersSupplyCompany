using System.Threading.Tasks;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IQueryHandler<TQueryParam, TReadModel>
    {
        Task<TReadModel> Handle(TQueryParam queryParam);
    }
}