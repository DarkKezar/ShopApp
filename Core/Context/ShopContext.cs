using Core.Models;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace Core.Context;

public class ShopContext : DbContext
{
    public DbSet<User>? Users { get; set; }
    public DbSet<Role>? Roles { get; set; }
    public DbSet<Category>? Categories { get; set; }
    public DbSet<Product>? Products { get; set; }
    public DbSet<ProductStats>? ProductStats { get; set; }
    public DbSet<ShoppingCart>? ShoppingCarts { get; set; }
    public DbSet<Order>? Orders { get; set; }
    
    public ShopContext()
    { }

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        Role adminRole = new Role { Id = new Guid("0cd70aac-7aa1-4f13-98de-3717e22dca1e"), Name = "Admin" },
            customerRole = new Role{Id = new Guid("92fa11b5-5cff-4cfd-ae99-fe975aaf2452"), Name = "Customer"};
        
        
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
        modelBuilder.Entity<Role>().HasData(adminRole, customerRole);

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
