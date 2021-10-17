using System.Threading.Tasks;

namespace PipefittersSupplyCompany.WebApi.Interfaces
{
    public interface IQueryResultHandler<TReadModel>
    {
        IQueryResultHandler<TReadModel> NextHandler { get; set; }
        void Process(ref IQueryResult<TReadModel> queryResult);
    }
}
