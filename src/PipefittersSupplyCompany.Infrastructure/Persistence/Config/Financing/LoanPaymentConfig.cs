using PipefittersSupplyCompany.Core.Financing.LoanAgreementAggregate;
using Microsoft.EntityFrameworkCore;
using PipefittersSupplyCompany.Core.Shared;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Persistence.Config.Financing
{
    internal class LoanPaymentConfig : IEntityTypeConfiguration<LoanPayment>
    {
        public void Configure(EntityTypeBuilder<LoanPayment> entity)
        {
            entity.ToTable("LoanPaymentSchedules", schema: "Finance");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("UNIQUEIDENTIFIER").HasColumnName("LoanPaymentId");
            entity.HasOne(p => p.EconomicEvent).WithOne().HasForeignKey<LoanPayment>(p => p.Id);
            // entity.HasOne(p => p.LoanAgreement).WithOne().HasForeignKey<LoanPayment>(p => p.Id);  Configured on the LoanAgreement side
            entity.Property(p => p.PaymentNumber)
                .HasConversion(p => p.Value, p => PaymentNumber.Create(p))
                .HasColumnType("int")
                .HasColumnName("PaymentNumber")
                .IsRequired();
            entity.Property(p => p.PaymentDueDate)
                .HasConversion(p => p.Value, p => PaymentDueDate.Create(p))
                .HasColumnType("DATETIME2(0)")
                .HasColumnName("PaymentDueDate")
                .IsRequired();
            entity.Property(p => p.LoanPrincipalAmount)
                .HasConversion(p => p.Value, p => LoanPrincipalAmount.Create(p))
                .HasColumnType("DECIMAL(18,2)")
                .HasColumnName("PrincipalAmount")
                .IsRequired();
            entity.Property(p => p.LoanInterestAmount)
                .HasConversion(p => p.Value, p => LoanInterestAmount.Create(p))
                .HasColumnType("DECIMAL(18,2)")
                .HasColumnName("InterestAmount")
                .IsRequired();
            entity.Property(p => p.LoanPrincipalRemaining)
                .HasConversion(p => p.Value, p => LoanPrincipalRemaining.Create(p))
                .HasColumnType("DECIMAL(18,2)")
                .HasColumnName("PrincipalRemaining")
                .IsRequired();
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