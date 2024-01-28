using DiyorMarket.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DiyorMarket.Infrastructure.Persistence.Configurations
{
    internal class SupplyEntityConfiguration : IEntityTypeConfiguration<Supply>
    {
        public void Configure(EntityTypeBuilder<Supply> builder)
        {
            builder.ToTable(nameof(Supply));

            builder.HasKey(s => s.Id);

            builder.HasOne(s => s.Supplier)
                .WithMany(sp => sp.Supplies)
                .HasForeignKey(s => s.SupplierId);

            builder.HasMany(s => s.SupplyItems)
                .WithOne(si => si.Supply)
                .HasForeignKey(si => si.SupplyId);
        }
    }
}
