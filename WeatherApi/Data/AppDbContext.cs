using Microsoft.EntityFrameworkCore;
using WeatherApi.Models;

namespace WeatherApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>()
            .Property(p => p.Price)
            .HasColumnType("decimal(18,2)");

        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Laptop", Price = 45000000 },
            new Product { Id = 2, Name = "Mouse", Price = 250000 },
            new Product { Id = 3, Name = "Keyboard", Price = 800000 },
            new Product { Id = 4, Name = "Monitor", Price = 12000000 },
            new Product { Id = 5, Name = "Headphone", Price = 3500000 }
        );
    }
}
