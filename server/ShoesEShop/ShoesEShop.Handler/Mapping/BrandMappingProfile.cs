using AutoMapper;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Brands.Dtos;

namespace ShoesEShop.Handler.Mapping
{
    internal class BrandMappingProfile : Profile
    {
        public BrandMappingProfile()
        {
            CreateMap<Brand, BrandDto>();
            CreateMap<BrandDto, Brand>();
        }
    }
}
