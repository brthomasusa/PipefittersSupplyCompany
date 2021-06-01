using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupply.Domain.Financing.CashDisbursement;


namespace PipefittersSupply.Infrastructure.Configuration
{
    internal class CashDisburseTypeConfig : IEntityTypeConfiguration<CashDisbursementType>
    {
        public void Configure(EntityTypeBuilder<CashDisbursementType> entity)
        {
            entity.ToTable("CashDisbursementTypes", schema: "Financing");
            entity.HasKey(e => e.CashDisbursementTypeId);
            entity.HasIndex(e => e.EventTypeName).IsUnique();
            entity.HasIndex(e => e.PayeeTypeName).IsUnique();

            entity.Property(prop => prop.EventTypeName).HasColumnType("nvarchar(25)");
            entity.Property(prop => prop.PayeeTypeName).HasColumnType("nvarchar(25)");

            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}