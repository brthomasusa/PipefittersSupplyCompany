using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace PipefittersSupplyCompany.Infrastructure.Persistence
{
    public class DapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionStr;

        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionStr = configuration.GetConnectionString("DefaultConnection");
        }

        public IDbConnection CreateConnection() => new SqlConnection(_connectionStr);
    }
}