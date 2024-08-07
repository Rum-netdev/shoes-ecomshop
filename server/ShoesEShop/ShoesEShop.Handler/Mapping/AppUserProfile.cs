using AutoMapper;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Auth.Commands;
using ShoesEShop.Handler.Shared.Dtos;

namespace ShoesEShop.Handler.Mapping
{
    public class AppUserProfile : Profile
    {
        public AppUserProfile()
        {
            CreateMap<AppUser, AppUserDto>()
                .ForMember(destinationMember: dest => dest.UserId, config => config.MapFrom(src => src.Id));
            CreateMap<AppUserDto, AppUser>()
                .ForMember(dest => dest.Id, config => config.MapFrom(src => src.UserId));
            CreateMap<RegisterCommand, AppUser>();
        }
    }
}
