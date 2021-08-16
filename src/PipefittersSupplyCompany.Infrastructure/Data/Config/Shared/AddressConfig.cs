using PipefittersSupplyCompany.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Data.Config.Shared
{
    internal class AddressConfig : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> entity)
        {
            entity.ToTable("Addresses", schema: "Shared");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("int").HasColumnName("AddressId");
            entity.Property(p => p.AddressLine1).HasColumnType("NVARCHAR(30)").HasColumnName("AddressLine1").IsRequired();
            entity.Property(p => p.AddressLine2).HasColumnType("NVARCHAR(30)").HasColumnName("AddressLine2");
            entity.Property(p => p.City).HasColumnType("NVARCHAR(30)").HasColumnName("City").IsRequired();
            entity.Property(p => p.StateCode).HasColumnType("NCHAR(2)").HasColumnName("StateCode").IsRequired();
            entity.Property(p => p.ZipCode).HasColumnType("NVARCHAR(10)").HasColumnName("Zipcode").IsRequired();
            entity.HasOne(e => e.Agent).WithMany(e => e.Addresses);
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}