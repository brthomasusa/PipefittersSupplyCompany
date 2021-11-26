using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Interfaces.HumanResources;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public class DoEmployeeDependencyCheckQuery
    {
        public async static Task<EmployeeDependencyCheckResult> Query(DoEmployeeDependencyCheck queryParameters, DapperContext ctx)
        {
            if (await IsValidEmployeeID(queryParameters.EmployeeID, ctx) == false)
            {
                throw new ArgumentException($"No employee record found where EmployeeId equals {queryParameters.EmployeeID}.");
            }

            var sql =
            @"SELECT 
                emp.EmployeeId, COUNT(DISTINCT addr.AddressId) AS 'Addresses', COUNT(DISTINCT con.PersonId) AS 'Contacts' 
            FROM HumanResources.Employees emp
            INNER JOIN Shared.Addresses addr ON emp.EmployeeId = addr.AgentId
            INNER JOIN Shared.ContactPersons con ON emp.EmployeeId = con.AgentId
            WHERE emp.EmployeeId = @ID
            GROUP BY emp.EmployeeId";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.EmployeeID, DbType.Guid);

            using (var connection = ctx.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<EmployeeDependencyCheckResult>(sql, parameters);
            }
        }

        private static async Task<bool> IsValidEmployeeID(Guid employeeId, DapperContext ctx)
        {
            string sql = $"SELECT EmployeeID FROM HumanResources.Employees WHERE EmployeeId = @ID";
            var parameters = new DynamicParameters();
            parameters.Add("ID", employeeId, DbType.Guid);

            using (var connection = ctx.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql, parameters);
                return result != null;
            }
        }
    }
}