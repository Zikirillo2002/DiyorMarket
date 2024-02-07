using DiyorMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiyorMarket.Infrastructure.Persistence.Configurations
{
    internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<User> builder)
        {
            builder.ToTable(nameof(User));
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(c => c.Phone)
                .HasMaxLength(255);

            builder.Property(c => c.Login)
                .IsRequired()
                .HasMaxLength(255)
                .HasAnnotation("MinLength", 8);

            builder.Property(c => c.Password)
                .IsRequired()
                .HasMaxLength(255)
                .HasAnnotation("MinLength", 8);
        }
    }
}
