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
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}