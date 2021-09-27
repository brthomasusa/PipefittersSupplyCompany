using System.Threading.Tasks;

namespace PipefittersSupplyCompany.Infrastructure.Interfaces
{
    public interface IQueryResultHandler
    {
        IQueryResultHandler NextHandler { get; set; }
        Task Process(string filename, string filecontent);
    }
}