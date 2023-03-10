using CustomerProducts.Core.DbContexts;
using CustomerProducts.Core.Entities; 

namespace CustomerProducts.Core.Services;

public class UserRepository : IUserRepository
{
    private readonly UserContext _context;

    public UserRepository(UserContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public User Validate(string userName, string password)
    {
        if (userName == null)
        {
            throw new ArgumentNullException(nameof(userName));
        }

        if (password == null)
        {
            throw new ArgumentNullException(nameof(password));
        }

        //var u = _context.Users.Where(u => u.UserName == userName && u.Password == password).FirstOrDefault();

        // always set the customerId to the passed-in customerId

        if (userName == "superuser" && password == "password")

            return new User("test", "user", "superuser", "password");

        return null;
    }
}

