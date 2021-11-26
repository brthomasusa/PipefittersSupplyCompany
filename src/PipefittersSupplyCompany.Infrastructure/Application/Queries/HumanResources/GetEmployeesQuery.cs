using System.Linq;
using System.Threading.Tasks;
using System.Data;
using Dapper;
using PipefittersSupplyCompany.Infrastructure.Persistence;

namespace PipefittersSupplyCompany.Infrastructure.Application.Queries.HumanResources
{
    public class GetEmployeesQuery
    {
        private static int Offset(int page, int pageSize) => (page - 1) * pageSize;

        public static async Task<PagedList<EmployeeListItem>> Query(GetEmployees queryParameters, DapperContext ctx)
        {
            var parameters = new DynamicParameters();

            var sql = "SELECT";
            sql += " ee.EmployeeId,  ee.LastName, ee.FirstName, ee.MiddleInitial, ee.Telephone, ee.IsActive, ee.SupervisorId,";
            sql += " supv.LastName AS ManagerLastName, supv.FirstName AS ManagerFirstName, supv.MiddleInitial AS ManagerMiddleInitial";
            sql += " FROM HumanResources.Employees ee";
            sql += " INNER JOIN";
            sql += " (";
            sql += " SELECT EmployeeId, LastName, FirstName, MiddleInitial";
            sql += " FROM HumanResources.Employees emp";
            sql += " WHERE EmployeeId IN (SELECT DISTINCT SupervisorId FROM HumanResources.Employees)";
            sql += " ) supv ON ee.SupervisorId = supv.EmployeeId WHERE 1 = 1";

            if (!string.IsNullOrEmpty(queryParameters.EmployeeLastName))
            {
                parameters.Add("EmployeeLastName", queryParameters.EmployeeLastName, DbType.String);
                sql += " AND ee.LastName LIKE @EmployeeLastName + '%'";
            }
            if (!string.IsNullOrEmpty(queryParameters.SupervisorLastName))
            {
                parameters.Add("SupervisorLastName", queryParameters.SupervisorLastName, DbType.String);
                sql += " AND supv.LastName LIKE @SupervisorLastName + '%'";
            }

            if (!string.IsNullOrEmpty(queryParameters.SortField))
            {
                if (!string.IsNullOrEmpty(queryParameters.SortOrder))
                {
                    sql += queryParameters.SortOrder.ToUpper() == "ASC"
                        ? $" ORDER BY {queryParameters.SortField} ASC"
                        : $" ORDER BY {queryParameters.SortField} DESC";
                }
                else
                {
                    sql += $" ORDER BY {queryParameters.SortField}";
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(queryParameters.SortOrder))
                {
                    sql += queryParameters.SortOrder.ToUpper() == "ASC"
                        ? " ORDER BY ee.LastName ASC"
                        : " ORDER BY ee.LastName DESC";
                }
                else
                {
                    sql += " ORDER BY ee.LastName";
                }
            }

            parameters.Add("Offset", Offset(queryParameters.Page, queryParameters.PageSize), DbType.Int32);
            parameters.Add("PageSize", queryParameters.PageSize, DbType.Int32);

            string stmt = sql + " OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";

            using (var connection = ctx.CreateConnection())
            {
                var items = await connection.QueryAsync<EmployeeListItem>(stmt, parameters);
                var totalItems = await connection.QueryAsync<EmployeeListItem>(sql, parameters);
                int count = totalItems.ToList().Count;

                var pagedList = PagedList<EmployeeListItem>.CreatePagedList(items.ToList(), count, queryParameters.Page, queryParameters.PageSize);
                return pagedList;
            }
        }
    }
}