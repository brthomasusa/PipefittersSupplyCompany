using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupply.Domain.HumanResources.Employees;
namespace PipefittersSupply.Infrastructure.Configuration
{
    internal class EmployeeTypeConfig : IEntityTypeConfiguration<EmployeeType>
    {
        public void Configure(EntityTypeBuilder<EmployeeType> entity)
        {

        }
    }
}