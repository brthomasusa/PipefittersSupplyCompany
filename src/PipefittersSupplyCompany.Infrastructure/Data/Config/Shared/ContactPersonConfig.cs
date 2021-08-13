using PipefittersSupplyCompany.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace PipefittersSupplyCompany.Infrastructure.Data.Config.Shared
{
    internal class ContactPersonConfig : IEntityTypeConfiguration<ContactPerson>
    {
        public void Configure(EntityTypeBuilder<ContactPerson> entity)
        {
            entity.ToTable("Persons", schema: "Shared");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("int").HasColumnName("PersonId");
            entity.Property(p => p.LastName).HasColumnType("NVARCHAR(25)").HasColumnName("LastName").IsRequired();
            entity.Property(p => p.FirstName).HasColumnType("NVARCHAR(25)").HasColumnName("FirstName").IsRequired();
            entity.Property(p => p.MiddleInitial).HasColumnType("NCHAR(1)").HasColumnName("MiddleInitial");
            entity.Property(p => p.Telephone).HasColumnType("NVARCHAR(14)").HasColumnName("Telephone").IsRequired();
            entity.Property(p => p.Notes).HasColumnType("NVARCHAR(1024)").HasColumnName("Notes").IsRequired();
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}