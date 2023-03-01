namespace Core.Models;

public class ShoppingCart
{
    public Guid Id { get; set; }
    public IEnumerable<Product> Products { get; set; }

    public ShoppingCart()
    {
        Products = new List<Product>();
    }
}