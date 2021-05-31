using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PipefittersSupply.Infrastructure.Configuration;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.HumanResources.TimeCards;
using PipefittersSupply.Domain.Purchasing.Inventory;
using PipefittersSupply.Domain.Purchasing.InventoryReceipt;
using PipefittersSupply.Domain.Purchasing.PurchaseOrder;
using PipefittersSupply.Domain.Purchasing.Vendor;
namespace PipefittersSupply.Infrastructure
{
    public class PipefittersSupplyDbContext : DbContext
    {
        private readonly ILogger _logger;

        public PipefittersSupplyDbContext(DbContextOptions<PipefittersSupplyDbContext> options, ILogger logger)
            : base(options)
        {
            _logger = logger;
        }

        public DbSet<EmployeeType> Documents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TimeCard> TimeCards { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}