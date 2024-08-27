using Microsoft.AspNetCore.Http;

namespace ShoesEShop.Handler.Services.Interfaces
{
    public interface IFileService
    {
        string GetTextFromFile(string filePath);
        string GetTextFromHttpFile(IFormFile file);
        Task<string> GetTextFromHttpFileAsync(IFormFile file);
        Task<List<string>> GetTextListFromHttpFileAsync(IFormFile file);
    }
}
