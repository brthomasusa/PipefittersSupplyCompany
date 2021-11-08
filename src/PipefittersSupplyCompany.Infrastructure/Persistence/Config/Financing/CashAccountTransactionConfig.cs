using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupplyCompany.Core.Shared;
using PipefittersSupplyCompany.Core.Financing.CashAccountAggregate;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Config.Financing
{
    internal class CashAccountTransactionConfig : IEntityTypeConfiguration<CashAccountTransaction>
    {
        public void Configure(EntityTypeBuilder<CashAccountTransaction> entity)
        {
            entity.ToTable("CashAccountTransactions", schema: "Finance");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("INT").HasColumnName("CashTransactionId");
            entity.Property(p => p.CashAccountId)
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("CashAccountId")
                .IsRequired();
            entity.Property(p => p.CashTransactionType)
                .HasColumnType("INT")
                .HasColumnName("CashTransactionTypeId")
                .IsRequired();
            entity.Property(p => p.CashAcctTransactionDate)
                .HasConversion(p => p.Value, p => CashAcctTransactionDate.Create(p))
                .HasColumnType("datetime2(0)")
                .HasColumnName("CashAcctTransactionDate")
                .IsRequired();
            entity.Property(p => p.CashAcctTransactionAmount)
                .HasConversion(p => p.Value, p => CashAcctTransactionAmount.Create(p))
                .HasColumnType("DECIMAL(18,2)")
                .HasColumnName("CashAcctTransactionAmount")
                .IsRequired();
            entity.Property(p => p.AgentId)
                .HasConversion(p => p.Value, p => ExternalAgentId.Create(p))
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("AgentId")
                .IsRequired();
            entity.Property(p => p.EventId)
                .HasConversion(p => p.Value, p => EconomicEventId.Create(p))
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("EventId")
                .IsRequired();
            entity.Property(p => p.CheckNumber)
                .HasConversion(p => p.Value, p => CheckNumber.Create(p))
                .HasColumnType("NVARCHAR(25)")
                .HasColumnName("CheckNumber")
                .IsRequired();
            entity.Property(p => p.RemittanceAdvice)
                .HasColumnType("NVARCHAR(50)")
                .HasColumnName("RemittanceAdvice");
            entity.Property(p => p.UserId)
                .HasConversion(p => p.Value, p => UserId.Create(p))
                .HasColumnType("UNIQUEIDENTIFIER")
                .HasColumnName("UserId")
                .IsRequired();
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}