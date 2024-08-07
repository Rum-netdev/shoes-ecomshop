using ShoesEShop.Handler.Shared.Dtos;
namespace ShoesEShop.Handler.Services.Interfaces
{
    public interface IJwtAuthenticationManager
    {
        string GenerateToken(AppUserDto user);
    }
}
