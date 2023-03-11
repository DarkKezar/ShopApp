using Core.Models;

namespace Core.Repositories.UserRepository;

public interface IUserRepository
{
    public Task<IQueryable<User>> GetAllUsersAsync();
    public Task<User> GetUserAsync(Guid id);
    public Task<User> CreateUserAsync(User user, string password);
    public Task<User> UpdateUserAsync(User user);
    public Task<User> UpdatePasswordAsync(User user, string password);
    public Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart);
    public Task DeleteUserAsync(User user);
    public Task<User> AuthorizeUserAsync(string login, string password);
}