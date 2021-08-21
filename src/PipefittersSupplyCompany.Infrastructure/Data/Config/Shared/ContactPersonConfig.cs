using PipefittersSupplyCompany.Core.Shared;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupplyCompany.SharedKernel.CommonValueObjects;

namespace PipefittersSupplyCompany.Infrastructure.Data.Config.Shared
{
    internal class ContactPersonConfig : IEntityTypeConfiguration<ContactPerson>
    {
        public void Configure(EntityTypeBuilder<ContactPerson> entity)
        {
            entity.ToTable("ContactPersons", schema: "Shared");
            entity.HasKey(e => e.Id);
            entity.Property(p => p.Id).HasColumnType("int").HasColumnName("PersonId");
            entity.OwnsOne(p => p.ContactName, p =>
            {
                p.Property(pp => pp.LastName).HasColumnType("NVARCHAR(25)").HasColumnName("LastName").IsRequired();
                p.Property(pp => pp.FirstName).HasColumnType("NVARCHAR(25)").HasColumnName("FirstName").IsRequired();
                p.Property(pp => pp.MiddleInitial).HasColumnType("NCHAR(1)").HasColumnName("MiddleInitial");
            });
            entity.Property(p => p.Telephone)
                .HasConversion(p => p.Value, p => PhoneNumber.Create(p))
                .HasColumnType("NVARCHAR(14)")
                .HasColumnName("Telephone")
                .IsRequired();
            entity.Property(p => p.Notes).HasColumnType("NVARCHAR(1024)").HasColumnName("Notes").IsRequired(false);
            entity.HasOne(e => e.Agent).WithMany(e => e.ContactPersons).OnDelete(DeleteBehavior.Cascade);
            entity.Property(e => e.CreatedDate)
                .HasColumnType("datetime2(7)")
                .ValueGeneratedOnAdd()
                .HasDefaultValueSql("sysdatetime()");
            entity.Property(e => e.LastModifiedDate).HasColumnType("datetime2(7)");
        }
    }
}