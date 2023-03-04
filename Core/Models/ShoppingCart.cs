namespace Core.Models;

public class ShoppingCart
{
    public Guid Id { get; set; }
    public List<Product> Products { get; set; }
    public User User { get; set; }

    public ShoppingCart()
    {
        Products = new List<Product>();
    }
}