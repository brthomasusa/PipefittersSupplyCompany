using System.Threading.Tasks;
using PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces.Financing
{
    public interface IStockSubscriptionAggregateRepository : IRepository<StockSubscription>
    {
        Task AddEconomicEventAsync(EconomicEvent economicEvent);
    }
}