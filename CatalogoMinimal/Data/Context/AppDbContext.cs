using CatalogoMinimal.Data.EntityTypeConfiguration;
using CatalogoMinimal.Models;
using Microsoft.EntityFrameworkCore;

namespace CatalogoMinimal.Data.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<Product> Product { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(EntityTypeConfiguration<>).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
