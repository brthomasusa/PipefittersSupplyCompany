using System;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PipefittersSupplyCompany.Infrastructure.Application.Queries;
using PipefittersSupplyCompany.Infrastructure.Persistence;
using PipefittersSupplyCompany.Infrastructure.Interfaces.Financing;
using PipefittersSupplyCompany.Infrastructure.Application.Queries.Financing;

namespace PipefittersSupplyCompany.Infrastructure.Application.Services.Financing
{
    public class FinancierQueryService : IFinancierQueryService
    {
        private readonly DapperContext _dapperCtx;

        public FinancierQueryService(DapperContext ctx) => _dapperCtx = ctx;

        private static int Offset(int page, int pageSize) => (page - 1) * pageSize;

        public async Task<FinancierDependencyCheckResult> Query(DoFinancierDependencyCheck queryParameters)
        {
            if (await IsValidFinancierID(queryParameters.FinancierID) == false)
            {
                throw new ArgumentException($"No financier record found where FinancierId equals {queryParameters.FinancierID}.");
            }

            var sql =
            @"SELECT 
                fin.FinancierID, COUNT(DISTINCT addr.AddressId) AS 'Addresses', COUNT(DISTINCT con.PersonId) AS 'Contacts' 
            FROM Finance.Financiers fin
            INNER JOIN Shared.Addresses addr ON fin.FinancierID = addr.AgentId
            INNER JOIN Shared.ContactPersons con ON fin.FinancierID = con.AgentId
            WHERE fin.FinancierID = @ID
            GROUP BY fin.FinancierID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.FinancierID, DbType.Guid);

            using (var connection = _dapperCtx.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<FinancierDependencyCheckResult>(sql, parameters);
            }
        }

        public async Task<PagedList<FinancierListItem>> Query(GetFinanciers queryParameters)
        {
            var sql =
            @"SELECT 
                fin.FinancierID, fin.FinancierName, fin.Telephone, fin.IsActive, fin.CreatedDate, fin.LastModifiedDate, users.UserName
            FROM Finance.Financiers fin
            INNER JOIN HumanResources.Users users ON fin.UserId = users.UserId
            ORDER BY fin.FinancierName
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            var totalRecordsSql = $"SELECT COUNT(FinancierId) FROM Finance.Financiers";

            using (var connection = _dapperCtx.CreateConnection())
            {
                int count = await connection.ExecuteScalarAsync<int>(totalRecordsSql);
                var items = await connection.QueryAsync<FinancierListItem>(sql, parameters);
                PagedList<FinancierListItem> pagedList = PagedList<FinancierListItem>.CreatePagedList(items.ToList(), count, queryParameters.Page, queryParameters.PageSize);
                return pagedList;
            }
        }

        public async Task<FinancierDetail> Query(GetFinancier queryParameters)
        {
            if (await IsValidFinancierID(queryParameters.FinancierID) == false)
            {
                throw new ArgumentException($"No financier record found where FinancierId equals {queryParameters.FinancierID}.");
            }

            var sql =
            @"SELECT 
                fin.FinancierID, fin.FinancierName, fin.Telephone, fin.IsActive, fin.CreatedDate, fin.LastModifiedDate, users.UserName
            FROM Finance.Financiers fin
            INNER JOIN HumanResources.Users users ON fin.UserId = users.UserId        
            WHERE fin.FinancierId = @ID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.FinancierID, DbType.Guid);

            using (var connection = _dapperCtx.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<FinancierDetail>(sql, parameters);
            }
        }

        public async Task<PagedList<FinancierAddressListItem>> Query(GetFinancierAddresses queryParameters)
        {
            if (await IsValidFinancierID(queryParameters.FinancierID) == false)
            {
                throw new ArgumentException($"No financier record found where FinancierId equals {queryParameters.FinancierID}.");
            }

            var sql =
            @"SELECT 
                aa.AddressId, fin.FinancierId, aa.AddressLine1 + ' ' + ISNULL(aa.AddressLine2, '') + ' ' + aa.City + ', ' + aa.StateCode + ' ' + aa.Zipcode AS FullAddress  
            FROM Finance.Financiers fin
            INNER JOIN Shared.Addresses aa ON fin.FinancierId = aa.AgentId
            WHERE fin.FinancierId = @ID           
            ORDER BY aa.AddressId
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.FinancierID, DbType.Guid);
            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            var totalRecordsSql =
            @"SELECT 
                COUNT(fin.FinancierId)
            FROM Finance.Financiers fin
            INNER JOIN Shared.Addresses aa ON fin.FinancierId = aa.AgentId
            WHERE fin.FinancierId = @ID";

            using (var connection = _dapperCtx.CreateConnection())
            {
                int count = await connection.ExecuteScalarAsync<int>(totalRecordsSql, parameters);
                var items = await connection.QueryAsync<FinancierAddressListItem>(sql, parameters);
                var pagedList = PagedList<FinancierAddressListItem>.CreatePagedList(items.ToList(), count, queryParameters.Page, queryParameters.PageSize);
                return pagedList;
            }
        }

        public async Task<FinancierAddressDetail> Query(GetFinancierAddress queryParameters)
        {
            var sql =
            @"SELECT 
                AddressId, AgentId AS 'FinancierId',  AddressLine1, AddressLine2, City, StateCode, Zipcode
            FROM Shared.Addresses       
            WHERE AddressId = @ID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.AddressID, DbType.Int32);

            using (var connection = _dapperCtx.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<FinancierAddressDetail>(sql, parameters);
            }
        }

        public async Task<PagedList<FinancierContactListItem>> Query(GetFinancierContacts queryParameters)
        {
            if (await IsValidFinancierID(queryParameters.FinancierID) == false)
            {
                throw new ArgumentException($"No Financier record found where FinancierId equals {queryParameters.FinancierID}.");
            }

            var sql =
            @"SELECT 
                PersonId, AgentId AS 'FinancierId', FirstName + ' ' + ISNULL(MiddleInitial, '') + ' ' + LastName AS 'FullName'  
            FROM Shared.ContactPersons
            WHERE AgentId = @ID           
            ORDER BY LastName, FirstName
            OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.FinancierID, DbType.Guid);
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
                var items = await connection.QueryAsync<FinancierContactListItem>(sql, parameters);
                var pagedList = PagedList<FinancierContactListItem>.CreatePagedList(items.ToList(), count, queryParameters.Page, queryParameters.PageSize);
                return pagedList;
            }
        }

        public async Task<FinancierContactDetail> Query(GetFinancierContact queryParameters)
        {
            var sql =
            @"SELECT 
                PersonId, AgentId AS 'FinancierId', LastName, FirstName, MiddleInitial, Telephone, Notes
            FROM Shared.ContactPersons       
            WHERE PersonId = @ID";

            var parameters = new DynamicParameters();
            parameters.Add("ID", queryParameters.PersonID, DbType.Int32);

            using (var connection = _dapperCtx.CreateConnection())
            {
                return await connection.QueryFirstOrDefaultAsync<FinancierContactDetail>(sql, parameters);
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

        private async Task<bool> IsValidFinancierID(Guid financierId)
        {
            string sql = $"SELECT FinancierID FROM Finance.Financiers WHERE FinancierId = @ID";
            var parameters = new DynamicParameters();
            parameters.Add("ID", financierId, DbType.Guid);

            using (var connection = _dapperCtx.CreateConnection())
            {
                var result = await connection.QueryFirstOrDefaultAsync(sql, parameters);
                return result != null;
            }
        }
    }
}