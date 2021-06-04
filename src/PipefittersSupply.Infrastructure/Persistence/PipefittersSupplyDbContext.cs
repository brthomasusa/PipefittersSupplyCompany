using System.Threading.Tasks;
using System.Reflection;
using Microsoft.EntityFrameworkCore;
using PipefittersSupply.Domain.HumanResources.Employees;
using PipefittersSupply.Domain.HumanResources.TimeCards;
using PipefittersSupply.Domain.Purchasing.Inventory;
using PipefittersSupply.Domain.Purchasing.InventoryReceipt;
using PipefittersSupply.Domain.Purchasing.PurchaseOrder;
using PipefittersSupply.Domain.Purchasing.Vendor;
using PipefittersSupply.Domain.Financing.CashDisbursement;
namespace PipefittersSupply.Infrastructure.Persistence
{
    public class PipefittersSupplyDbContext : DbContext
    {
        public PipefittersSupplyDbContext(DbContextOptions<PipefittersSupplyDbContext> options)
            : base(options) { }

        public DbSet<EmployeeType> EmployeeTypes { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<TimeCard> TimeCards { get; set; }
        public DbSet<PurchaseOrder> PurchaseOrders { get; set; }
        public DbSet<PurchaseOrderDetail> PurchaseOrderDetails { get; set; }
        public DbSet<CashDisbursementType> CashDisbursementTypes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            // modelBuilder.ApplyConfiguration(new EmployeeTypeConfig());
        }
    }
}