using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesEShop.Data.Entities;

namespace ShoesEShop.Data.Configurations
{
    public class ProductCategoryConfiguration : IEntityTypeConfiguration<ProductCategory>
    {
        public void Configure(EntityTypeBuilder<ProductCategory> builder)
        {
            builder.ToTable("ProductCategories");
            builder.HasKey(k => new {k.ProductId, k.CategoryId});

            builder.HasOne(s => s.Product)
                .WithMany(d => d.ProductCategories)
                .HasForeignKey(fk => fk.ProductId);

            builder.HasOne(s => s.Category)
                .WithMany(d => d.ProductCategories)
                .HasForeignKey(fk => fk.CategoryId);
        }
    }
}
