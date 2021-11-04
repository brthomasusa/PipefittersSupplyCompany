using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;
using PipefittersSupplyCompany.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Config.Shared
{
    internal class EconomicEventsConfig : IEntityTypeConfiguration<EconomicEvent>
    {
        public void Configure(EntityTypeBuilder<EconomicEvent> entity)
        {
            entity.ToTable("EconomicEvents", schema: "Shared");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("EventId");
            entity.Property(p => p.EventType).HasColumnType("int").HasColumnName("EventTypeId").IsRequired();

            // This maps primary key Economic.EventId to foreign key LoanAgreement.LoanId; it has to be defined on
            // both ends because LoanAgreement does not have an EventId property that would allow efcore to automatically
            // configure the relationship.
            // entity.HasOne(e => e.LoanAgreement).WithOne(e => e.EconomicEvent).HasForeignKey<LoanAgreement>(e => e.Id); <-- Uncomment this for 2-way navigation

            // This maps primary key Economic.EventId to foreign key LoanPayment.LoanPaymentId; it has to be defined on
            // both ends because LoanPayment does not have an EventId property that would allow efcore to automatically
            // configure the relationship.            
            // entity.HasOne(e => e.LoanPayment).WithOne(e => e.EconomicEvent).HasForeignKey<LoanPayment>(e => e.Id); <-- Uncomment this for 2-way navigation
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}