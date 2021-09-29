using System.Threading.Tasks;

namespace PipefittersSupplyCompany.WebApi.Interfaces
{
    public interface IQueryResultHandler
    {
        IQueryResultHandler NextHandler { get; set; }
        void Process(ref IQueryResult queryResult);
    }
}
