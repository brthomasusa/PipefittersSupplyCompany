using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupply.Domain.Financing.CashDisbursement;


namespace PipefittersSupply.Infrastructure.Persistence.Configuration
{
    internal class CashDisburseTypeConfig : IEntityTypeConfiguration<CashDisbursementType>
    {
        public void Configure(EntityTypeBuilder<CashDisbursementType> entity)
        {
            entity.ToTable("CashDisbursementTypes", schema: "Financing");
            entity.HasKey(e => e.CashDisbursementTypeId);
            entity.Property(p => p.CashDisbursementTypeId).ValueGeneratedNever();

            entity.OwnsOne(e => e.Id)
                .Property(p => p.Value)
                .HasColumnType("int")
                .IsRequired();
            entity.OwnsOne(e => e.EventTypeName)
                .Property(p => p.Value)
                .HasColumnType("nvarchar(25)")
                .HasColumnName("EventTypeName")
                .IsRequired();
            entity.OwnsOne(e => e.PayeeTypeName)
                .Property(p => p.Value)
                .HasColumnType("nvarchar(25)")
                .HasColumnName("PayeeTypeName")
                .IsRequired();
            entity.OwnsOne(e => e.CreatedDate)
                .Property(p => p.Value)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()")
                .HasColumnName("CreatedDate")
                .IsRequired();
            entity.OwnsOne(e => e.LastModifiedDate)
                .Property(p => p.Value)
                .HasColumnType("datetime2(7)")
                .HasColumnName("LastModifiedDate");
        }
    }
}