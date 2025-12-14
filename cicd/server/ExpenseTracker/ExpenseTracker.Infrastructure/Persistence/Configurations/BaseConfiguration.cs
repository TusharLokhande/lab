using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ExpenseTracker.Infrastructure.Persistence.Configurations
{
    public abstract class BaseConfiguration<TEntity> : IEntityTypeConfiguration<TEntity>
        where TEntity : BaseEntity
    {
        public virtual void Configure(EntityTypeBuilder<TEntity> builder)
        {
            var utcConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),              // when saving → enforce UTC
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc) // when reading → mark as UTC
            );

            var utcNullableConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? v.Value.ToUniversalTime() : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v
            );

            builder.Property(e => e.IsActive)
                    .HasDefaultValue(true)
                    .IsRequired();

            builder.Property(e => e.CreatedAt)
                .HasConversion(utcConverter);

            builder.Property(e => e.UpdatedAt)
                .HasConversion(utcNullableConverter);
        }

    }
}
