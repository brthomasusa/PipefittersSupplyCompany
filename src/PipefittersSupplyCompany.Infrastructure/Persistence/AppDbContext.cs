using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Ardalis.EFCore.Extensions;
using PipefittersSupplyCompany.Core.ProjectAggregate;
using PipefittersSupplyCompany.Core.HumanResources;
using PipefittersSupplyCompany.Core.HumanResources.EmployeeAggregate;
using PipefittersSupplyCompany.SharedKernel;
using PipefittersSupplyCompany.Core.Shared;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace PipefittersSupplyCompany.Infrastructure.Persistence
{
    public class AppDbContext : DbContext
    {
        private readonly IMediator _mediator;

        public AppDbContext(DbContextOptions<AppDbContext> options, IMediator mediator)
            : base(options)
        {
            _mediator = mediator;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        public DbSet<ToDoItem> ToDoItems { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<ExternalAgent> ExternalAgents { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ContactPerson> ContactPersons { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly();
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            int result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

            // ignore events if no dispatcher provided
            if (_mediator == null) return result;

            // dispatch events only if save was successful
            var entitiesWithEvents = ChangeTracker.Entries<BaseEntity<int>>()
                .Select(e => e.Entity)
                .Where(e => e.Events.Any())
                .ToArray();

            foreach (var entity in entitiesWithEvents)
            {
                var events = entity.Events.ToArray();
                entity.Events.Clear();
                foreach (var domainEvent in events)
                {
                    await _mediator.Publish(domainEvent).ConfigureAwait(false);
                }
            }

            return result;
        }

        public override int SaveChanges()
        {
            return SaveChangesAsync().GetAwaiter().GetResult();
        }
    }
}