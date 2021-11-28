using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using PipefittersSupplyCompany.Infrastructure.Persistence;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public class GetSupervisorLookup
    {
        public static async Task<ReadOnlyCollection<SupervisorLookup>> Query(DapperContext ctx)
        {
            var sql = "SELECT ee.EmployeeId AS ManagerId,";
            sql += " CONCAT(ee.FirstName,' ',COALESCE(ee.MiddleInitial,''),' ',ee.LastName) as ManagerName ";
            sql += " FROM HumanResources.Employees ee";
            sql += " WHERE ee.EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)";
            sql += " ORDER BY ee.LastName, ee.FirstName, ee.MiddleInitial";

            using var connection = ctx.CreateConnection();
            var items = await connection.QueryAsync<SupervisorLookup>(sql);

            return new ReadOnlyCollection<SupervisorLookup>(items.ToList());
        }
    }
}