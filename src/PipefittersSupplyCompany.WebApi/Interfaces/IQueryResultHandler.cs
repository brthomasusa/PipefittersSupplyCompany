using System.Threading.Tasks;

namespace PipefittersSupplyCompany.WebApi.Interfaces
{
    public interface IQueryResultHandler
    {
        IQueryResultHandler NextHandler { get; set; }
        Task Process(IQueryResult queryResult);
    }
}