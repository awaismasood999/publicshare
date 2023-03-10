using AutoMapper;
using CustomerProducts.Core.Entities;

namespace CustomerProducts.API.Profiles;
public class ProductProfile : Profile
{
    public ProductProfile()
    {
        CreateMap<Product, Models.ProdcutDto>();
        CreateMap<Models.ProductForCreationDto, Product>();
    }
}