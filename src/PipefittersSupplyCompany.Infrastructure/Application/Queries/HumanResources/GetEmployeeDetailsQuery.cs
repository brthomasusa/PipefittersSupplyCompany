using System;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PipefittersSupplyCompany.Infrastructure.Persistence;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public class GetEmployeeDetailsQuery
    {
        public static async Task<EmployeeDetail> Query(GetEmployee queryParameters, DapperContext ctx)
        {
            if (await IsValidEmployeeID(queryParameters.EmployeeID, ctx) == false)
            {
                throw new ArgumentException($"No employee record found where EmployeeId equals {queryParameters.EmployeeID}.");
            }

            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, 
                CONCAT(ee.FirstName,' ',COALESCE(ee.MiddleInitial,''),' ',ee.LastName) as EmployeeFullName, ee.Telephone, ee.IsActive,
                ee.SupervisorId, CONCAT(supv.FirstName,' ',COALESCE(supv.MiddleInitial,''),' ',supv.LastName) as SupervisorFullName,
                ee.SSN, ee.MaritalStatus, ee.Exemptions, ee.PayRate, ee.StartDate, ee.CreatedDate, ee.LastModifiedDate                
            FROM HumanResources.Employees ee
            INNER JOIN
            (
                SELECT 
                    EmployeeId, LastName, FirstName, MiddleInitial 
                FROM HumanResources.Employees emp
                WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)
            ) supv ON ee.SupervisorId = supv.EmployeeId          
            WHERE ee.EmployeeId = @ID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.EmployeeID, DbType.Guid);

            using (var connection = ctx.CreateConnection())
            {
                EmployeeDetail employee = await connection.QueryFirstOrDefaultAsync<EmployeeDetail>(sql, parameters);

                employee.Addresses =
                    await GetEmployeeDetailAddresses.Query(new GetEmployeeAddresses { EmployeeID = queryParameters.EmployeeID }, ctx);

                employee.Contacts =
                    await GetEmployeeDetailContacts.Query(new GetEmployeeContacts { EmployeeID = queryParameters.EmployeeID }, ctx);

                return employee;
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