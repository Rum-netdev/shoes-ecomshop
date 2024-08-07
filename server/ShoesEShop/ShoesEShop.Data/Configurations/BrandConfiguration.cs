using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesEShop.Data.Entities;

namespace ShoesEShop.Data.Configurations
{
    public class BrandConfiguration : IEntityTypeConfiguration<Brand>
    {
        public void Configure(EntityTypeBuilder<Brand> builder)
        {
            builder.ToTable("Brands");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.Name).IsRequired();
        }
    }
}
