using Microsoft.EntityFrameworkCore;
using MicroserviceArchitecture.Services.Customer.Model.Entities;
using MicroserviceArchitecture.Services.Customer.Infra.SqlServer.Mapping;
using Microsoft.Extensions.Configuration;

namespace MicroserviceArchitecture.Services.Customer.Infra.SqlServer.ContextDb
{
    public partial class CustomerDbContext : DbContext
    {
        private readonly IConfiguration configuration;
        public virtual DbSet<Client> Clients { get; set; }
        public CustomerDbContext()
        {

        }
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options, IConfiguration configuration) : base(options)
        {
            this.configuration = configuration;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new CustomerMapping());

            base.OnModelCreating(modelBuilder);
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("ServiceDb"));
        }

        public async override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.CurrentValues["IsRemoved"] = false;
                        entry.CurrentValues["CreatedAt"] = DateTime.Now;
                        break;
                    case EntityState.Deleted:
                        entry.State = EntityState.Modified;
                        entry.CurrentValues["IsRemoved"] = true;
                        entry.CurrentValues["RemovedAt"] = DateTime.Now;
                        break;
                }
            }
            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}