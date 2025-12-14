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
    public class UserRoleMappingConfigurations : BaseConfiguration<UserRoleMapping>
    {
        public override void Configure(EntityTypeBuilder<UserRoleMapping> builder)
        {
            builder.ToTable("UserRoleMappings");

            builder.HasKey(urm => urm.Id);

            builder
                .HasOne(urm => urm.User)
                .WithMany(urm => urm.UserRoleMappings)
                .HasForeignKey(urm => urm.UserId);

            builder
                .HasOne(urm => urm.Role)
                .WithMany(r => r.UserRoleMappings)
                .HasForeignKey(urm => urm.RoleId);

            builder
                .HasIndex(urm => new { urm.UserId, urm.RoleId })
                .IsUnique();
        }
    }
}
