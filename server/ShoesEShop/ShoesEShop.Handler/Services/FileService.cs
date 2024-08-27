using Microsoft.AspNetCore.Http;
using ShoesEShop.Handler.Services.Interfaces;

namespace ShoesEShop.Handler.Services
{
    public class FileService : IFileService
    {
        public string GetTextFromFile(string filePath)
        {
            return File.ReadAllText(filePath);
        }

        public string GetTextFromHttpFile(IFormFile file)
        {
            string text = "";
            using MemoryStream ms = new MemoryStream();
            file.CopyTo(ms);

            using (StreamReader sr = new StreamReader(ms))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null) 
                {
                    text += $"{line}\n";
                }
            }

            return text;
        }

        public async Task<string> GetTextFromHttpFileAsync(IFormFile file)
        {
            string text = "";
            using var fs = file.OpenReadStream();

            using (StreamReader sr = new StreamReader(fs))
            {
                string line = "";
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    text += $"{line}\n";
                }
            }

            return text;
        }

        public async Task<List<string>> GetTextListFromHttpFileAsync(IFormFile file)
        {
            List<string> texts = new List<string>();
            using var fs = file.OpenReadStream();

            using (StreamReader sr = new StreamReader(fs))
            {
                string line = "";
                while (!sr.EndOfStream)
                {
                    line = sr.ReadLine();
                    texts.Add(line);
                }
            }

            return texts;
        }
    }
}
