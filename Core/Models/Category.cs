namespace Core.Models;

public class Category
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    //public List<Product> Products { get; set; }

    public Category()
    { }

    public Category(string name)
    {
        this.Name = name;
    }
}