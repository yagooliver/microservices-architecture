using MicroserviceArchitecture.Services.Customer.Model.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MicroserviceArchitecture.Services.Customer.Infra.SqlServer.Mapping
{
    public class CustomerMapping : IEntityTypeConfiguration<Client>
    {
        /// <summary>
        /// Configure mapper for client entity
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Client> builder)
        {
            builder.ToTable("customer");

            builder.HasKey(e => e.Id);

            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name")
                .HasColumnOrder(2)
                .IsRequired();

            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name")
                .HasColumnOrder(3)
                .IsRequired();

            builder.Property(x => x.Email)
                .HasMaxLength(100)
                .HasColumnName("email")
                .HasColumnOrder(4)
                .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasColumnName("created_at")
                .HasColumnOrder(5)
                .IsRequired();

            builder.Property(e => e.CreatedBy)
                .HasColumnName("created_by")
                .HasColumnOrder(6)
                .IsRequired();

            builder.Property(e => e.IsRemoved)
                .HasColumnName("is_removed")
                .HasColumnOrder(7)
                .IsRequired();

            builder.Property(e => e.RemovedAt)
                .HasColumnName("removed_at")
                .HasColumnOrder(8)
                .IsRequired(false);

            builder.Property(e => e.RemovedBy)
                .HasColumnName("removedBy")
                .HasColumnOrder(9)
                .IsRequired(false);

            builder.HasQueryFilter(x => !x.IsRemoved);
        }
    }
}