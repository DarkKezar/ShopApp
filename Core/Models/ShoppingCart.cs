namespace Core.Models;

public class ShoppingCart
{
    public Guid Id { get; set; }
    public User User { get; set; }
    public IEnumerable<Product> Products { get; set; }

    public ShoppingCart(User user)
    {
        User = user;
    }
}