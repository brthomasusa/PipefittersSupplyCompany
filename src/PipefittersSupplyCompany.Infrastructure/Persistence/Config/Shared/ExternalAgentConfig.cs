using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.Core.Financing.FinancierAggregate;
using PipefittersSupplyCompany.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Config.Shared
{
    public class ExternalAgentConfig : IEntityTypeConfiguration<ExternalAgent>
    {
        public void Configure(EntityTypeBuilder<ExternalAgent> entity)
        {
            entity.ToTable("ExternalAgents", schema: "Shared");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("AgentId");
            entity.Property(p => p.AgentType).HasColumnType("int").HasColumnName("AgentTypeId").IsRequired();
            entity.HasOne(e => e.Employee).WithOne(e => e.ExternalAgent).HasForeignKey<Employee>(e => e.Id);
            entity.HasOne(e => e.Financier).WithOne(e => e.ExternalAgent).HasForeignKey<Financier>(e => e.Id);
            entity.HasMany(e => e.Addresses).WithOne(a => a.Agent);
            entity.HasMany(e => e.ContactPersons).WithOne(a => a.Agent);
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}