using Microsoft.AspNetCore.Http;

namespace ShoesEShop.Handler.Services.Interfaces
{
    public interface IFileStorageService
    {
        string SaveFile(IFormFile file);
        Task<string> SaveFileAsync(IFormFile file);
    }
}
