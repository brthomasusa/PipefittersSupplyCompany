using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PipefittersSupply.Infrastructure.Interfaces;
using static PipefittersSupply.Infrastructure.Queries.HumanResources.ReadModels;
using static PipefittersSupply.Infrastructure.Queries.HumanResources.QueryParameters;

namespace PipefittersSupply.Infrastructure.Application.Services
{
    public class EmployeeQueryService : IEmployeeQueryService
    {
        private readonly IDbConnection _connection;

        public EmployeeQueryService(IDbConnection connection) => _connection = connection;

        public async Task<IEnumerable<EmployeeListItems>> Query(GetEmployees queryParameters)
        {
            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive,
                eeType.EmployeeTypeName AS [Role], ee.SupervisorId, supv.LastName AS ManagerLastName,
                supv.FirstName AS ManagerFirstName, supv.MiddleInitial AS ManagerMiddleInitial 
            FROM HumanResources.Employees ee
            INNER JOIN
            (
                SELECT 
                    EmployeeId, LastName, FirstName, MiddleInitial 
                FROM HumanResources.Employees emp
                WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)
            ) supv ON ee.SupervisorId = supv.EmployeeId
            INNER JOIN HumanResources.EmployeeTypes eetype ON ee.EmployeeType = eetype.EmployeeTypeId            
            ORDER BY ee.LastName, ee.FirstName, ee.MiddleInitial 
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            using (_connection)
            {
                return await _connection.QueryAsync<EmployeeListItems>(sql, parameters);
            }
        }

        public async Task<IEnumerable<EmployeeListItems>> Query(GetEmployeesSupervisedBy queryParameters)
        {
            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive,
                eeType.EmployeeTypeName AS [Role], ee.SupervisorId, supv.LastName AS ManagerLastName,
                supv.FirstName AS ManagerFirstName, supv.MiddleInitial AS ManagerMiddleInitial 
            FROM HumanResources.Employees ee
            INNER JOIN
            (
                SELECT 
                    EmployeeId, LastName, FirstName, MiddleInitial 
                FROM HumanResources.Employees emp
                WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)
            ) supv ON ee.SupervisorId = supv.EmployeeId
            INNER JOIN HumanResources.EmployeeTypes eetype ON ee.EmployeeType = eetype.EmployeeTypeId            
            WHERE ee.SupervisorId = @ID
            ORDER BY ee.LastName, ee.FirstName, ee.MiddleInitial            
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.SupervisorID, DbType.Int32);
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            using (_connection)
            {
                return await _connection.QueryAsync<EmployeeListItems>(sql, parameters);
            }
        }

        public async Task<IEnumerable<EmployeeListItems>> Query(GetEmployeesOfEmployeeType queryParameters)
        {
            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive,
                eeType.EmployeeTypeName AS [Role], ee.SupervisorId, supv.LastName AS ManagerLastName,
                supv.FirstName AS ManagerFirstName, supv.MiddleInitial AS ManagerMiddleInitial 
            FROM HumanResources.Employees ee
            INNER JOIN
            (
                SELECT 
                    EmployeeId, LastName, FirstName, MiddleInitial 
                FROM HumanResources.Employees emp
                WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)
            ) supv ON ee.SupervisorId = supv.EmployeeId
            INNER JOIN HumanResources.EmployeeTypes eetype ON ee.EmployeeType = eetype.EmployeeTypeId            
            WHERE ee.EmployeeType = @ID
            ORDER BY ee.LastName, ee.FirstName, ee.MiddleInitial            
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.EmployeeTypeID, DbType.Int32);
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            using (_connection)
            {
                return await _connection.QueryAsync<EmployeeListItems>(sql, parameters);
            }
        }

        public async Task<EmployeeDetails> Query(GetEmployee queryParameters)
        {
            var sql =
            @"SELECT 
                ee.EmployeeId,  eeType.EmployeeTypeName AS [Role], ee.LastName, ee.FirstName, 
                ee.MiddleInitial, ee.SupervisorId, supv.LastName AS ManagerLastName,
                supv.FirstName AS ManagerFirstName, supv.MiddleInitial AS ManagerMiddleInitial, ee.SSN,
                ee.AddressLine1, ee.AddressLine2, ee.City, ee.Zipcode, ee.Telephone, ee.MaritalStatus,
                ee.TaxExemptions, ee.PayRate, ee.StartDate, ee.IsActive, ee.CreatedDate, ee.LastModifiedDate
            FROM HumanResources.Employees ee
            INNER JOIN
            (
                SELECT 
                    EmployeeId, LastName, FirstName, MiddleInitial 
                FROM HumanResources.Employees emp
                WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)
            ) supv ON ee.SupervisorId = supv.EmployeeId
            INNER JOIN HumanResources.EmployeeTypes eetype ON ee.EmployeeType = eetype.EmployeeTypeId            
            WHERE ee.EmployeeId = @ID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.EmployeeID, DbType.Int32);

            using (_connection)
            {
                return await _connection.QueryFirstOrDefaultAsync<EmployeeDetails>(sql, parameters);
            }


        }

        private static int Offset(int page, int pageSize) => (page - 1) * pageSize;
    }
}