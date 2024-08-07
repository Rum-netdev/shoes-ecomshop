using AutoMapper;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Products.Dtos;

namespace ShoesEShop.Handler.Mapping
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(des => des.BrandName, m => m.MapFrom(src => src.Brand.Name));
            CreateMap<ProductDto, Product>();
        }
    }
}
