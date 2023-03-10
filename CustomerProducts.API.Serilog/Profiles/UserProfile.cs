using AutoMapper;
using CustomerProducts.Core.Entities;

namespace CustomerProducts.API.JWT.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<User, Models.UserDto>();
        }
    }
}
