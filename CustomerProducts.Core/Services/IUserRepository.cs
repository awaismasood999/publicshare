using CustomerProducts.Core.Entities; 

namespace CustomerProducts.Core.Services;

public interface IUserRepository
{
    User Validate(string userName, string passoword);
    
}

