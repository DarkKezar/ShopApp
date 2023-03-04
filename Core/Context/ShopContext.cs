using Core.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Core.Context;

public class ShopContext : DbContext
{
    public Microsoft.EntityFrameworkCore.DbSet<User> Users { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<Role> Roles { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<Category> Categories { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<Product> Products { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<ProductStats> ProductStats { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<ShoppingCart> ShoppingCarts { get; set; }
    public Microsoft.EntityFrameworkCore.DbSet<Order> Orders { get; set; }
    
    public ShopContext()
    { }

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<User>().HasKey(u => u.Id);
        modelBuilder.Entity<User>().HasAlternateKey(u => u.Login);
        modelBuilder.Entity<User>().Property(u => u.Login).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.PasswordHash).IsRequired();
        modelBuilder.Entity<User>().Property(u => u.IsDeleted).IsRequired().HasDefaultValue(false);
        modelBuilder.Entity<User>()
            .HasOne(u => u.ShoppingCart)
            .WithOne(s => s.User).HasForeignKey<ShoppingCart>(o => o.Id);
        modelBuilder.Entity<User>()
            .HasMany(u => u.Orders)
            .WithOne(o => o.User);
        modelBuilder.Entity<User>()
            .HasMany(u => u.Roles)
            .WithMany(r => r.Users);

        modelBuilder.Entity<Role>().HasKey(r => r.Id);
        modelBuilder.Entity<Role>().Property(r => r.Name).IsRequired();

        modelBuilder.Entity<Category>().HasKey(c => c.Id);
        modelBuilder.Entity<Category>().Property(c => c.Name).IsRequired();

        modelBuilder.Entity<Product>().HasKey(p => p.Id);
        modelBuilder.Entity<Product>().Property(p => p.Name).IsRequired();
        modelBuilder.Entity<Product>().Property(p => p.Price).IsRequired().HasDefaultValue(0);

        modelBuilder.Entity<ProductStats>().HasKey(p => p.Id);
        modelBuilder.Entity<ProductStats>().Property(p => p.PhotoUrl).IsRequired();
        modelBuilder.Entity<ProductStats>().Property(p => p.SomeData).IsRequired();
        
        modelBuilder.Entity<Order>().HasKey(o => o.Id);
        modelBuilder.Entity<Order>().Property(o => o.Status).IsRequired().HasDefaultValue(Order.StatusType.New);
       // modelBuilder.Entity<Order>().Property(o => o.User);

        modelBuilder.Entity<ShoppingCart>().HasKey(s => s.Id);
        
        base.OnModelCreating(modelBuilder);
    }
}