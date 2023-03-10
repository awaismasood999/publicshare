using System.ComponentModel.DataAnnotations;

namespace CustomerProducts.API.Models;

public class CustomerForCreationDto
{
    [Required(ErrorMessage = "You should fill out a FristName.")]
    public string FirstName { get; set; } = string.Empty;

    [Required(ErrorMessage = "You should fill out a LastName.")]
    public string LastName { get; set; } = string.Empty;

    [Required(ErrorMessage = "You should fill out a Address.")]
    public string Address { get; set; } = string.Empty;
    public ICollection<ProductForCreationDto> Products { get; set; }
        = new List<ProductForCreationDto>();

}
