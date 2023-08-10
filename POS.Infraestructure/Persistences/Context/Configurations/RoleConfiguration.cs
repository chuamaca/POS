using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using POS.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace POS.Infraestructure.Persistences.Context.Configurations
{
    public class RoleConfiguration : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasKey(e => e.RoleId).HasName("PK__Roles__8AFACE1A408A95B4");

            builder.Property(e => e.Description)
                .HasMaxLength(50)
                .IsUnicode(false);
        }
    }
}
