using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CustomerProducts.Core.Entities;

public class Product
{
    [Key]
    public Guid Id { get; set; }
     
    [Required]
    [MaxLength(100)]
    public string Name { get; set; }

    [MaxLength(1500)]
    public string? Description { get; set; }

    [ForeignKey("CustomerId")]
    public Customer Customer { get; set; } = null!;

    public Guid CustomerId { get; set; }

    public Product(string name)
    {
        Name = name; 
    }
}

