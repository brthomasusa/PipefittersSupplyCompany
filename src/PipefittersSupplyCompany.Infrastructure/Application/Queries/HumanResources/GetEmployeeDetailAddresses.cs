using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PipefittersSupplyCompany.Infrastructure.Persistence;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public class GetEmployeeDetailAddresses
    {
        public static async Task<List<EmployeeAddressDetail>> Query(GetEmployeeAddresses queryParameters, DapperContext ctx)
        {
            if (await IsValidEmployeeID(queryParameters.EmployeeID, ctx) == false)
            {
                throw new ArgumentException($"No employee record found where EmployeeId equals {queryParameters.EmployeeID}.");
            }

            var sql =
            @"SELECT 
            a.AddressId, AgentId AS 'EmployeeId',  a.AddressLine1, a.AddressLine2, a.City, a.StateCode, a.Zipcode,
            CONCAT(a.AddressLine1,' ',COALESCE(a.AddressLine2,''), ' ', a.City, ', ', a.StateCode, ' ', a.Zipcode) as FullAddress
            FROM Shared.Addresses a      
            WHERE a.AgentId = @ID 
            ORDER BY a.City, a.StateCode, a.Zipcode, a.AddressLine1";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.EmployeeID, DbType.Guid);

            using (var connection = ctx.CreateConnection())
            {
                var items = await connection.QueryAsync<EmployeeAddressDetail>(sql, parameters);
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