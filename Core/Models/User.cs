namespace Core.Models;

public class User
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public IEnumerable<Role> Roles { get; set; }
    public string Login { get; set; }
    public string PasswordHash { get; set; }
    
    public ShoppingCart ShoppingCart { get; set; }
    public IEnumerable<Order> Orders { get; set; }

    public User(string name, string login)
    {
        Name = name;
        Login = login;
    }
}