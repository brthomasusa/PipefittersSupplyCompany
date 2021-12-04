using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PipefittersSupplyCompany.Infrastructure.Persistence;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public class GetEmployeeDetailContacts
    {
        public static async Task<List<EmployeeContactDetail>> Query(GetEmployeeContacts queryParameters, DapperContext ctx)
        {
            if (await IsValidEmployeeID(queryParameters.EmployeeID, ctx) == false)
            {
                throw new ArgumentException($"No employee record found where EmployeeId equals {queryParameters.EmployeeID}.");
            }

            var sql =
            @"SELECT 
                PersonId, AgentId AS 'EmployeeId', Telephone, Notes,
                CONCAT(FirstName,' ',COALESCE(MiddleInitial,''),' ',LastName) AS FullName                
            FROM Shared.ContactPersons       
            WHERE AgentId = @ID 
            ORDER BY LastName, FirstName";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.EmployeeID, DbType.Guid);

            using (var connection = ctx.CreateConnection())
            {
                var items = await connection.QueryAsync<EmployeeContactDetail>(sql, parameters);
                return items.ToList();
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
                return (result != null);
            }
        }
    }
}