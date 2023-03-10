
namespace CustomerProducts.API.Models;
public class ProdcutDto
{
    public Guid Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}
