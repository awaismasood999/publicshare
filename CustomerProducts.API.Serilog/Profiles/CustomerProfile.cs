using AutoMapper;
using CustomerProducts.Core.Entities;

namespace CustomerProducts.API.Profiles;
public class CustomerProfile : Profile
{
    public CustomerProfile()
    {
        CreateMap<Customer, Models.CustomerDto>()
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));

        CreateMap<Models.CustomerForCreationDto, Customer>();

    }
}

