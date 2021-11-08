using System.Threading.Tasks;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.SharedKernel.Interfaces;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces.Financing
{
    public interface ILoanAgreementAggregateRepository : IRepository<LoanAgreement>
    {
        Task AddEconomicEventAsync(EconomicEvent economicEvent);
    }
}