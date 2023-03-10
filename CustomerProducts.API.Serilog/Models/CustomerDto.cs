namespace CustomerProducts.API.Models;

public class CustomerDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; }

    public IEnumerable<ProdcutDto> Products { get; set; } =  Enumerable.Empty<ProdcutDto>();

}
