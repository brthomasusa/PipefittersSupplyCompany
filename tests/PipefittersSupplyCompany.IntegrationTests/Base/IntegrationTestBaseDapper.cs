using Microsoft.Extensions.Configuration;
using TestSupport.Helpers;
using PipefittersSupplyCompany.Infrastructure.Persistence;

namespace PipefittersSupplyCompany.IntegrationTests.Base
{
    public class IntegrationTestBaseDapper
    {
        protected readonly DapperContext _dapperCtx;
        protected readonly string serviceAddress = "https://localhost:5001/";

        public IntegrationTestBaseDapper()
        {
            var config = AppSettings.GetConfiguration();
            _dapperCtx = new DapperContext(config.GetConnectionString("DefaultConnection"));
        }
    }
}