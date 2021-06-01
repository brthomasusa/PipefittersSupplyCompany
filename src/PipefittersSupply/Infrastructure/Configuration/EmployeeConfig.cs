using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupply.Domain.HumanResources.Employees;

namespace PipefittersSupply.Infrastructure.Configuration
{
    internal class EmployeeConfig : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> entity)
        {
            entity.ToTable("Employees", schema: "HumanResources");
            entity.HasKey(e => e.EmployeeId);
            entity.OwnsOne(e => e.Id);
            entity.OwnsOne(e => e.EmployeeTypeId);
            entity.OwnsOne(e => e.SupervisorId);
            entity.OwnsOne(e => e.LastName);
            entity.OwnsOne(e => e.FirstName);
            entity.OwnsOne(e => e.MiddleInitial);
            entity.OwnsOne(e => e.SSN);
            entity.OwnsOne(e => e.AddressLine1);
            entity.OwnsOne(e => e.AddressLine2);
            entity.OwnsOne(e => e.City);
            entity.OwnsOne(e => e.State);
            entity.OwnsOne(e => e.Zipcode);
            entity.OwnsOne(e => e.Telephone);
            entity.OwnsOne(e => e.MaritalStatus);
            entity.OwnsOne(e => e.Exemptions);
            entity.OwnsOne(e => e.PayRate);
            entity.OwnsOne(e => e.StartDate);
            entity.OwnsOne(e => e.IsActive);
            entity.OwnsOne(e => e.CreatedDate);
            entity.OwnsOne(e => e.LastModifiedDate);
        }
    }
}