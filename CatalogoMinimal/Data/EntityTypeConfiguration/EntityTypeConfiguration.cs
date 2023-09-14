using CatalogoMinimal.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CatalogoMinimal.Data.EntityTypeConfiguration
{
    public abstract class EntityTypeConfiguration<T> : IEntityTypeConfiguration<T> where T : Entity<Guid>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(e => e.Id);
        }
    }
}
