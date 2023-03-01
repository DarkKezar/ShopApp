using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Context;

public class ShopContext : DbContext
{
    public DbSet<User> Users;
    public DbSet<Role> Roles;
    public DbSet<Category> Categories;
    public DbSet<Product> Products;
    public DbSet<ProductStats> ProductStats;
    public DbSet<ShoppingCart> ShoppingCarts;
    public DbSet<Order> Orders;
    
    public ShopContext()
    { }

    public ShopContext(DbContextOptions<ShopContext> options) : base(options)
    { }
}