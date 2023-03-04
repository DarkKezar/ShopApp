namespace Infrastructure.DTO.ProductTO;

public class CreateProductTO
{
    public string Name { get; set; }
    public List<Guid> CategoriesId { get; set; }
    public double Price { get; set; }
    
    public string SomeData { get; set; }
    public string PhotoUrl { get; set; }
}