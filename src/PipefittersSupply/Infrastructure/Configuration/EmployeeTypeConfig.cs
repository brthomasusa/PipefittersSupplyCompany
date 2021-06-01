using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupply.Domain.HumanResources.Employees;
namespace PipefittersSupply.Infrastructure.Configuration
{
    internal class EmployeeTypeConfig : IEntityTypeConfiguration<EmployeeType>
    {
        public void Configure(EntityTypeBuilder<EmployeeType> entity)
        {
            entity.HasKey(e => e.EmployeeTypeId);
            entity.OwnsOne(e => e.Id);
            entity.OwnsOne(e => e.EmployeeTypeName);
            entity.OwnsOne(e => e.CreatedDate);
            entity.OwnsOne(e => e.LastModifiedDate);
        }
    }
}