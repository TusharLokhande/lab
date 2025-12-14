using ExpenseTracker.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpenseTracker.Infrastructure.Persistence.Configurations
{
    internal class AuthProviderConfiguration: BaseConfiguration<AuthProvider>
    {
        public override void Configure(EntityTypeBuilder<AuthProvider> builder)
        {
            builder.ToTable("AuthProviders");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                   .ValueGeneratedNever();

            builder.Property(x => x.Provider)
                   .IsRequired();

            builder.Property(x => x.ProviderUserId)
                   .HasMaxLength(200)
                   .IsRequired();

            // One user -> many auth providers
            builder.HasOne(x => x.User)
                   .WithMany(x => x.AuthProviders)
                   .HasForeignKey(x => x.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            // Ensure no duplicate provider for same user
            builder.HasIndex(x => new { x.UserId, x.Provider })
                   .IsUnique();

            // Ensure ProviderUserId is unique per provider
            builder.HasIndex(x => new { x.Provider, x.ProviderUserId })
                   .IsUnique();
        }
    }
}
