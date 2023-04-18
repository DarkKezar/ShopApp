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
    public User? User { get; set; }
    public List<Product>? Products { get; set; }
    public StatusType Status { get; set; }

    
    public Order(){}
    public Order(User user, List<Product> products)
    {
        User = user;
        Products = products;
        Status = StatusType.New;
    }
}