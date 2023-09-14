using CatalogoMinimal.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoMinimal.Data.EntityTypeConfiguration
{
    public class ProductEntityTypeConfiguration : EntityTypeConfiguration<Product>
    {
        public override void Configure(EntityTypeBuilder<Product> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(55);
            builder.Property(x => x.Description).HasMaxLength(255);
            builder.Property(x => x.Price).HasPrecision(14, 2);
            builder.Property(x => x.Image);

            builder.HasOne(x => x.Category)
                    .WithMany(x => x.Products)
                    .HasForeignKey(x => x.CategoryId);
        }
    }
}
