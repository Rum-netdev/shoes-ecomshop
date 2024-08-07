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
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.ToTable("Products");
            builder.HasKey(k => k.Id);

            builder.Property(p => p.ProductName).IsRequired();
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.Quantity).IsRequired();

            builder.HasOne(s => s.Brand)
                .WithMany(d => d.Products)
                .HasForeignKey(fk => fk.Id);
        }
    }
}
