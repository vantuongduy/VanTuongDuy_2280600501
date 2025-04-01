using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace VanTuongDuy_2280600501.Models
{
   
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Name = "Ghế", Price = 30000, },
                new Product { Id = 2, Name = "Tủ gỗ", Price = 40000, },
                new Product { Id = 3, Name = "Giường", Price = 40000, }
            );
        }
    }


    }

