namespace Infrastructure.DTO.ProductTO;

public class UpdateProductTO
{
    public Guid Id { get; set; }
    public CreateProductTO NewData { get; set; }
}