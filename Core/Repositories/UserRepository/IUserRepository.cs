using Core.Models;

namespace Core.Repositories.UserRepository;

public interface IUserRepository
{
    public Task<IEnumerable<User>> GetAllUsersAsync(int count, int page);
    public Task<User> GetUserAsync(Guid id);
    public Task<User> CreateUserAsync(User user, string password);
    public Task<User> UpdateUserAsync(User user);
    public Task<ShoppingCart> UpdateShoppingCartAsync(ShoppingCart shoppingCart);
    public Task DeleteUserAsync(User user);
    public Task<bool> AuthorizateUserAsync(string login, string password);
}