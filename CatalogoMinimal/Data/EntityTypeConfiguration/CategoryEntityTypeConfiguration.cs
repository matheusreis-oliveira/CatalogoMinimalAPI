using CatalogoMinimal.Models;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoMinimal.Data.EntityTypeConfiguration
{
    public class CategoryEntityTypeConfiguration : EntityTypeConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            base.Configure(builder);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(55);
            builder.Property(x => x.Description).HasMaxLength(255);
        }
    }
}
