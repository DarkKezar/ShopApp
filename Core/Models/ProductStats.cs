namespace Core.Models;

public class ProductStats
{
    public Guid Id { get; set; }
    public Product Product { get; set; }
    
    public string SomeData { get; set; }
    public string PhotoUrl { get; set; }

    public ProductStats(string photoUrl, string someData)
    {
        PhotoUrl = photoUrl;
        SomeData = someData;
    }
}