using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Context;

public class ShopContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    public DbSet<Category> Categories { get; set; }
    public DbSet<Product> Products { get; set; }
    public DbSet<ProductStats> ProductStats { get; set; }
    public DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public DbSet<Order> Orders { get; set; }
    
    public ShopContext()
    { }

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    { }

    /* fluentApi here
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<User>().   
    }
    */
}