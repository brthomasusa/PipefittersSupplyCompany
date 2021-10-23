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

namespace PipefittersSupplyCompany.Infrastructure.Application.Services.HumanResources
{
    public class EmployeeQueryService : IEmployeeQueryService
    {
        private readonly DapperContext _dapperCtx;

        public EmployeeQueryService(DapperContext ctx) => _dapperCtx = ctx;

        private static int Offset(int page, int pageSize) => (page - 1) * pageSize;

        public async Task<PagedList<EmployeeListItem>> Query(GetEmployees queryParameters)
        {
            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive, ee.SupervisorId,
                supv.LastName AS ManagerLastName, supv.FirstName AS ManagerFirstName, supv.MiddleInitial AS ManagerMiddleInitial 
            FROM HumanResources.Employees ee
            INNER JOIN
            (
                SELECT 
                    EmployeeId, LastName, FirstName, MiddleInitial 
                FROM HumanResources.Employees emp
                WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)
            ) supv ON ee.SupervisorId = supv.EmployeeId          
            ORDER BY ee.LastName, ee.FirstName, ee.MiddleInitial
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            var totalRecordsSql = $"SELECT COUNT(EmployeeId) FROM HumanResources.Employees";

            using (var connection = _dapperCtx.CreateConnection())
            {
                int count = await connection.ExecuteScalarAsync<int>(totalRecordsSql);
                var items = await connection.QueryAsync<EmployeeListItem>(sql, parameters);
                var pagedList = PagedList<EmployeeListItem>.CreatePagedList(items.ToList(), count, queryParameters.Page, queryParameters.PageSize);
                return pagedList;
            }
        }

        public async Task<PagedList<EmployeeListItem>> Query(GetEmployeesSupervisedBy queryParameters)
        {
            if (await IsValidSupervisorID(queryParameters.SupervisorID) == false)
            {
                throw new ArgumentException($"{queryParameters.SupervisorID} is not a valid supervisor Id.");
            }

            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive, ee.SupervisorId,
                supv.LastName AS ManagerLastName, supv.FirstName AS ManagerFirstName, supv.MiddleInitial AS ManagerMiddleInitial 
            FROM HumanResources.Employees ee
            INNER JOIN
            (
                SELECT 
                    EmployeeId, LastName, FirstName, MiddleInitial 
                FROM HumanResources.Employees emp
                WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)
            ) supv ON ee.SupervisorId = supv.EmployeeId
            WHERE ee.SupervisorId = @ID           
            ORDER BY ee.LastName, ee.FirstName, ee.MiddleInitial
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var totalRecordsSql = @"SELECT COUNT(EmployeeId) FROM HumanResources.Employees WHERE SupervisorId = @ID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.SupervisorID, DbType.Guid);
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);


            using (var connection = _dapperCtx.CreateConnection())
            {
                int count = await connection.ExecuteScalarAsync<int>(totalRecordsSql, parameters);
                var items = await connection.QueryAsync<EmployeeListItem>(sql, parameters);
                var pagedList = PagedList<EmployeeListItem>.CreatePagedList(items.ToList(), count, queryParameters.Page, queryParameters.PageSize);
                return pagedList;
            }
        }

        public async Task<PagedList<EmployeeListItemWithRoles>> Query(GetEmployeesOfRole queryParameters)
        {
            if (await IsValidRoleID(queryParameters.RoleID) == false)
            {
                throw new ArgumentException($"{queryParameters.RoleID} is not a valid role Id.");
            }

            var sql =
            @"SELECT 
                ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive,
                rnames.RoleId, rnames.RoleName AS [Role], ee.SupervisorId, supv.LastName AS ManagerLastName,
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

            var totalRecordsSql =
            @"SELECT 
                COUNT(ee.EmployeeId)
            FROM HumanResources.Employees ee
            INNER JOIN HumanResources.UserRoles uroles ON ee.EmployeeId = uroles.UserId
            WHERE uroles.RoleId = @ID";

            using (var connection = _dapperCtx.CreateConnection())
            {
                int count = await connection.ExecuteScalarAsync<int>(totalRecordsSql, parameters);
                var items = await connection.QueryAsync<EmployeeListItemWithRoles>(sql, parameters);
                var pagedList = PagedList<EmployeeListItemWithRoles>.CreatePagedList(items.ToList(), count, queryParameters.Page, queryParameters.PageSize);
                return pagedList;
            }
        }

        public async Task<EmployeeDetail> Query(GetEmployee queryParameters)
        {
            if (await IsValidEmployeeID(queryParameters.EmployeeID) == false)
            {
                throw new ArgumentException($"No employee record found where EmployeeId equals {queryParameters.EmployeeID}.");
            }

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
                return await connection.QueryFirstOrDefaultAsync<EmployeeDetail>(sql, parameters);
            }
        }

        public async Task<PagedList<EmployeeAddressListItem>> Query(GetEmployeeAddresses queryParameters)
        {
            if (await IsValidEmployeeID(queryParameters.EmployeeID) == false)
            {
                throw new ArgumentException($"No employee record found where EmployeeId equals {queryParameters.EmployeeID}.");
            }

            var sql =
            @"SELECT 
                aa.AddressId, ee.EmployeeId, aa.AddressLine1 + ' ' + ISNULL(aa.AddressLine2, '') + ' ' + aa.City + ', ' + aa.StateCode + ' ' + aa.Zipcode AS FullAddress  
            FROM HumanResources.Employees ee
            INNER JOIN Shared.Addresses aa ON ee.EmployeeId = aa.AgentId
            WHERE ee.EmployeeId = @ID           
            ORDER BY aa.AddressId
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.EmployeeID, DbType.Guid);
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            var totalRecordsSql =
            @"SELECT 
                COUNT(ee.EmployeeId)
            FROM HumanResources.Employees ee
            INNER JOIN Shared.Addresses aa ON ee.EmployeeId = aa.AgentId
            WHERE ee.EmployeeId = @ID";

            using (var connection = _dapperCtx.CreateConnection())
            {
                int count = await connection.ExecuteScalarAsync<int>(totalRecordsSql, parameters);
                var items = await connection.QueryAsync<EmployeeAddressListItem>(sql, parameters);
                var pagedList = PagedList<EmployeeAddressListItem>.CreatePagedList(items.ToList(), count, queryParameters.Page, queryParameters.PageSize);
                return pagedList;
            }
        }

        public async Task<EmployeeAddressDetail> Query(GetEmployeeAddress queryParameters)
        {
            var sql =
            @"SELECT 
                AddressId, AgentId AS 'EmployeeId',  AddressLine1, AddressLine2, City, StateCode, Zipcode
            FROM Shared.Addresses       
            WHERE AddressId = @ID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.AddressID, DbType.Int32);

            using (var connection = _dapperCtx.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<EmployeeAddressDetail>(sql, parameters);
            }
        }

        public async Task<PagedList<EmployeeContactListItem>> Query(GetEmployeeContacts queryParameters)
        {
            if (await IsValidEmployeeID(queryParameters.EmployeeID) == false)
            {
                throw new ArgumentException($"No employee record found where EmployeeId equals {queryParameters.EmployeeID}.");
            }

            var sql =
            @"SELECT 
                PersonId, AgentId AS 'EmployeeId', FirstName + ' ' + ISNULL(MiddleInitial, '') + ' ' + LastName AS 'FullName'  
            FROM Shared.ContactPersons
            WHERE AgentId = @ID           
            ORDER BY LastName, FirstName
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.EmployeeID, DbType.Guid);
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            var totalRecordsSql =
            @"SELECT 
                COUNT(AgentId)
            FROM Shared.ContactPersons
            WHERE AgentId = @ID";

            using (var connection = _dapperCtx.CreateConnection())
            {
                int count = await connection.ExecuteScalarAsync<int>(totalRecordsSql, parameters);
                var items = await connection.QueryAsync<EmployeeContactListItem>(sql, parameters);
                var pagedList = PagedList<EmployeeContactListItem>.CreatePagedList(items.ToList(), count, queryParameters.Page, queryParameters.PageSize);
                return pagedList;
            }
        }

        public async Task<EmployeeContactDetail> Query(GetEmployeeContact queryParameters)
        {
            var sql =
            @"SELECT 
                PersonId, AgentId AS 'EmployeeId', LastName, FirstName, MiddleInitial, Telephone, Notes
            FROM Shared.ContactPersons       
            WHERE PersonId = @ID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.PersonID, DbType.Int32);

            using (var connection = _dapperCtx.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<EmployeeContactDetail>(sql, parameters);
            }
        }

        private async Task<bool> IsValidEmployeeID(Guid employeeId)
        {
            string sql = $"SELECT EmployeeID FROM HumanResources.Employees WHERE EmployeeId = @ID";
            var parameters = new DynamicParameters();
            parameters.Add("ID", employeeId, DbType.Guid);

            using (var connection = _dapperCtx.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql, parameters);
                return result != null;
            }
        }

        private async Task<bool> IsValidSupervisorID(Guid supervisorId)
        {
            string sql = $"SELECT EmployeeID FROM HumanResources.Employees WHERE SupervisorId = @ID";
            var parameters = new DynamicParameters();
            parameters.Add("ID", supervisorId, DbType.Guid);

            using (var connection = _dapperCtx.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql, parameters);
                return result != null;
            }
        }

        private async Task<bool> IsValidRoleID(Guid roleId)
        {
            string sql = $"SELECT RoleID FROM HumanResources.Roles WHERE RoleId = @ID";
            var parameters = new DynamicParameters();
            parameters.Add("ID", roleId, DbType.Guid);

            using (var connection = _dapperCtx.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql, parameters);
                return result != null;
            }
        }
    }
}