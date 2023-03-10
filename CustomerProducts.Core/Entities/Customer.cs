using System.ComponentModel.DataAnnotations;

namespace CustomerProducts.Core.Entities;

public class Customer
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; }

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; }

    [Required]
    [MaxLength(50)]
    public string Address { get; set; }

    public ICollection<Product> Products { get; set; }
        = new List<Product>();

    public Customer(string firstName, string lastName, string address)
    { 
        FirstName = firstName;
        LastName = lastName;
        Address = address;
    }

    public Customer() { }
}

