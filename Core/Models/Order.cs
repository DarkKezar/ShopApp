namespace Core.Models;

public class Order
{
    public enum StatusType { 
                    New, 
                    InProgress, 
                    Completed, 
                    Canceled
                };
    
    public Guid Id { get; set; }
    public User User { get; set; }
    public IEnumerable<Product> Products { get; set; }
    public StatusType Status { get; set; }

    public Order(User user, IEnumerable<Product> products)
    {
        User = user;
        Products = products;
        Status = StatusType.New;
    }
}