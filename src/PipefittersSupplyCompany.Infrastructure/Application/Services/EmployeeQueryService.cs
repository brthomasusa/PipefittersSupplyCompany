using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Interfaces;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.ReadModels;
using static PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources.QueryParameters;

namespace PipefittersSupplyCompany.Infrastructure.Application.Services
{
    public class EmployeeQueryService : IEmployeeQueryService
    {
        private readonly DapperContext _dapperCtx;

        public EmployeeQueryService(DapperContext ctx) => _dapperCtx = ctx;

        private static int Offset(int page, int pageSize) => (page - 1) * pageSize;

        public async Task<IEnumerable<EmployeeListItems>> Query(GetEmployees queryParameters)
        {
            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive,
                rnames.RoleName AS [Role], ee.SupervisorId, supv.LastName AS ManagerLastName,
                supv.FirstName AS ManagerFirstName, supv.MiddleInitial AS ManagerMiddleInitial 
            FROM HumanResources.Employees ee
            INNER JOIN
            (
                SELECT 
                    EmployeeId, LastName, FirstName, MiddleInitial 
                FROM HumanResources.Employees emp
                WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)
            ) supv ON ee.SupervisorId = supv.EmployeeId
            INNER JOIN HumanResources.UserRoles uroles ON ee.EmployeeId = uroles.UserId
            INNER JOIN HumanResources.Roles rnames ON uroles.RoleId = rnames.RoleId           
            ORDER BY ee.LastName, ee.FirstName, ee.MiddleInitial
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            using (var connection = _dapperCtx.CreateConnection())
            {
                return await connection.QueryAsync<EmployeeListItems>(sql, parameters);
            }
        }

        public async Task<IEnumerable<EmployeeListItems>> Query(GetEmployeesSupervisedBy queryParameters)
        {
            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive,
                rnames.RoleName AS [Role], ee.SupervisorId, supv.LastName AS ManagerLastName,
                supv.FirstName AS ManagerFirstName, supv.MiddleInitial AS ManagerMiddleInitial 
            FROM HumanResources.Employees ee
            INNER JOIN
            (
                SELECT 
                    EmployeeId, LastName, FirstName, MiddleInitial 
                FROM HumanResources.Employees emp
                WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)
            ) supv ON ee.SupervisorId = supv.EmployeeId
            INNER JOIN HumanResources.UserRoles uroles ON ee.EmployeeId = uroles.UserId
            INNER JOIN HumanResources.Roles rnames ON uroles.RoleId = rnames.RoleId
            WHERE ee.SupervisorId = @ID           
            ORDER BY ee.LastName, ee.FirstName, ee.MiddleInitial
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.SupervisorID, DbType.Guid);
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            using (var connection = _dapperCtx.CreateConnection())
            {
                return await connection.QueryAsync<EmployeeListItems>(sql, parameters);
            }
        }

        public async Task<IEnumerable<EmployeeListItems>> Query(GetEmployeesOfRole queryParameters)
        {
            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive,
                rnames.RoleName AS [Role], ee.SupervisorId, supv.LastName AS ManagerLastName,
                supv.FirstName AS ManagerFirstName, supv.MiddleInitial AS ManagerMiddleInitial 
            FROM HumanResources.Employees ee
            INNER JOIN
            (
                SELECT 
                    EmployeeId, LastName, FirstName, MiddleInitial 
                FROM HumanResources.Employees emp
                WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)
            ) supv ON ee.SupervisorId = supv.EmployeeId
            INNER JOIN HumanResources.UserRoles uroles ON ee.EmployeeId = uroles.UserId
            INNER JOIN HumanResources.Roles rnames ON uroles.RoleId = rnames.RoleId
            WHERE rnames.RoleId = @ID           
            ORDER BY ee.LastName, ee.FirstName, ee.MiddleInitial
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            // 
            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.RoleID, DbType.Guid);
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            using (var connection = _dapperCtx.CreateConnection())
            {
                return await connection.QueryAsync<EmployeeListItems>(sql, parameters);
            }
        }

        public async Task<EmployeeDetails> Query(GetEmployee queryParameters)
        {
            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive,
                ee.SupervisorId, supv.LastName AS ManagerLastName, supv.FirstName AS ManagerFirstName,
                supv.MiddleInitial AS ManagerMiddleInitial, ee.SSN, ee.MaritalStatus, ee.Exemptions,
                ee.PayRate, ee.StartDate, ee.IsActive, ee.CreatedDate, ee.LastModifiedDate
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

            using (var connection = _dapperCtx.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<EmployeeDetails>(sql, parameters);
            }
        }
    }
}