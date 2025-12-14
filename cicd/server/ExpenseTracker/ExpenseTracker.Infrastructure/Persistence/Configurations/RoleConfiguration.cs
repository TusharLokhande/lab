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
    public class RoleConfiguration: BaseConfiguration<Role>
    {
        public override  void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.ToTable("Roles");

            builder.Property(s => s.Name)
                   .HasMaxLength(50)
                   .IsRequired();

            builder.Property(s => s.Code)
                   .HasMaxLength(50)
                   .IsRequired();
        }
    }
}
