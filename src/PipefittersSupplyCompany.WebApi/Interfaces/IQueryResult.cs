using PipefittersSupplyCompany.Infrastructure.Interfaces;
namespace PipefittersSupplyCompany.WebApi.Interfaces
{
    public interface IQueryResult
    {
        IReadModel ReadModel { get; set; }
    }
}