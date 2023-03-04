using Core.Models;

namespace Infrastructure.DTO.ProductTO;

public class GetProductTO
{
    public List<Product> Products { get; set; }
}