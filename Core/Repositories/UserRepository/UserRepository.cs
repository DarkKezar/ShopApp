using Core.Context;
using Core.Models;
using Microsoft.EntityFrameworkCore;

namespace Core.Repositories.UserRepository;

public class UserRepository : IUserRepository
{
    private readonly ShopContext _context;

    private string PasswordHashGenerator(string password)
    {
        /*
         * Hash function
         */
        return password;
    }
    
    public UserRepository(ShopContext context)
    {
        _context = context;
    }

    public async Task<IQueryable<User>> GetAllUsersAsync()
    {
        return _context.Users;
    }

    public async Task<IQueryable<User>> GetUserAsync(Guid id)
    {
        return _context.Users.Where(u => u.Id == id);
    }

    public async Task<User> CreateUserAsync(User user, string password)
    {
        user.PasswordHash = PasswordHashGenerator(password);
        await _context.Users.AddAsync(user);
        return user;
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<User> UpdatePasswordAsync(User user, string password)
    {
        user.PasswordHash = PasswordHashGenerator(password);
        return await this.UpdateUserAsync(user);
    }

    public async Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart)
    {
        _context.ShoppingCarts.Update(shoppingCart);
        await _context.SaveChangesAsync();
        return shoppingCart;
    }

    public async Task DeleteUserAsync(User user)
    {
        user.IsDeleted = true;
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User> AuthorizeUserAsync(string login, string password)
    {
        User? user = await _context.Users.SingleOrDefaultAsync(u => u.Login.Equals(login));
        if (user == null  || user.IsDeleted) return null;
        else if (user.PasswordHash.Equals(PasswordHashGenerator(password))) return user;
        else return null;
    }
}