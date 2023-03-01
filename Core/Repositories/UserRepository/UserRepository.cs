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

    public async Task<IEnumerable<User>> GetAllUsersAsync(int count, int page)
    {
        return _context.Users.Skip(count * page).Take(count);
    }

    public async Task<User> GetUserAsync(Guid id)
    {
        return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
    }

    public async Task<User> CreateUserAsync(User user, string password)
    {
        throw new NotImplementedException();
    }

    public async Task<User> UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
        return user;
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

    public async Task<bool> AuthorizateUserAsync(string login, string password)
    {
        /*
         * Here I only check login and password, that's why I return boolean
         * JWT I will generate in IUserService (or IAccountService)
         */
        
        User user = await _context.Users.SingleOrDefaultAsync(u => u.Login.Equals(login));
        if (user.IsDeleted) return false;
        else return user.PasswordHash.Equals(PasswordHashGenerator(password));
    }
}