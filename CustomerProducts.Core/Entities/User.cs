using System.ComponentModel.DataAnnotations;

namespace CustomerProducts.Core.Entities;

public class User
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
    public string UserName { get; set; }

    [Required]
    [MaxLength(50)]
    public string Password { get; set; }
    public User(string firstName, string lastName, string userName, string password)
    {
        FirstName = firstName;
        LastName = lastName;
        UserName = userName;
        Password = password;
    }
}

