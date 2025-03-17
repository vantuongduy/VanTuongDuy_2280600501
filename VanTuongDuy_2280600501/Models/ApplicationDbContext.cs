using Microsoft.EntityFrameworkCore;

namespace VanTuongDuy_2280600501.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Thường" },
                new Category { Id = 2, Name = "Tốt" },
                new Category { Id = 3, Name = "Cao Cấp" }
            );
        }
    }
}
