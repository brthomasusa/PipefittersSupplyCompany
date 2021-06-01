using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PipefittersSupply.Domain.HumanResources.TimeCards;

namespace PipefittersSupply.Infrastructure.Configuration
{
    internal class TimeCardConfig : IEntityTypeConfiguration<TimeCard>
    {
        public void Configure(EntityTypeBuilder<TimeCard> entity)
        {
            entity.ToTable("TimeCards", schema: "HumanResources");
            entity.HasKey(e => e.TimeCardId);
            entity.OwnsOne(e => e.Id);
            entity.OwnsOne(e => e.EmployeeId);
            entity.OwnsOne(e => e.SupervisorId);
            entity.OwnsOne(e => e.PayPeriodEnded);
            entity.OwnsOne(e => e.RegularHours);
            entity.OwnsOne(e => e.OvertimeHours);
            entity.OwnsOne(e => e.CreatedDate);
            entity.OwnsOne(e => e.LastModifiedDate);
        }
    }
}