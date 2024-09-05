using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShoesEShop.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesEShop.Data.Configurations
{
    public class ProductImageConfiguration : IEntityTypeConfiguration<ProductImage>
    {
        public void Configure(EntityTypeBuilder<ProductImage> builder)
        {
            builder.ToTable("ProductImages");
            builder.HasKey(x => x.Id);

            builder.Property(t => t.Id)
                .UseIdentityColumn()
                .ValueGeneratedOnAdd();

            builder.Property(t => t.Urls).IsRequired();

            builder.HasOne(t => t.Product)
                .WithMany(s => s.ProductImages)
                .HasForeignKey(fk => fk.ProductId);
        }
    }
}
