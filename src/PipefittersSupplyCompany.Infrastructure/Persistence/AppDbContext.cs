using System.Reflection;
using PipefittersSupplyCompany.Core.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;
using PipefittersSupplyCompany.Core.Financing.CashAccountAggregate;
using PipefittersSupplyCompany.Core.Financing.StockSubscriptionAggregate;
using PipefittersSupplyCompany.Core.Shared;
using Microsoft.EntityFrameworkCore;

namespace PipefittersSupplyCompany.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        // protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        // {
        //     optionsBuilder.UseLazyLoadingProxies();
        // }

        public DbSet<Role> Roles { get; set; }
        public DbSet<ExternalAgent> ExternalAgents { get; set; }
        public DbSet<EconomicEvent> EconomicEvents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Financier> Financiers { get; set; }
        public DbSet<LoanAgreement> LoanAgreements { get; set; }
        public DbSet<LoanPayment> LoanPaymentSchedules { get; set; }
        public DbSet<CashAccount> CashAccounts { get; set; }
        public DbSet<CashAccountTransaction> CashAccountTransactions { get; set; }
        public DbSet<StockSubscription> StockSubscriptions { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}