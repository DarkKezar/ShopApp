using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public List<Category>? Categories { get; set; }
    public ProductStats? ProductStats { get; set; }
    public double Price { get; set; }

    public Product() {}
    public Product(string name, List<Category> categories, 
                    double price, ProductStats productStats)
    {
        Name = name;
        Categories = categories;
        Price = price;
        ProductStats = productStats;
    }
    
}