using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using ShoesEShop.Handler.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesEShop.Handler.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IHostingEnvironment _environment;
        readonly string _guacHomePath;

        public FileStorageService(IHostingEnvironment environment)
        {
            _environment = environment;
            _guacHomePath = Path.Combine(_environment.WebRootPath, "Images", "Products");
        }

        public string SaveFile(IFormFile file)
        {
            string newFileName = DateTime.Now.Ticks + "_" + Guid.NewGuid().ToString();
            Directory.CreateDirectory(_guacHomePath);
            var filePath = Path.Combine(_guacHomePath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            return filePath;
        }

        public async Task<string> SaveFileAsync(IFormFile file)
        {
            string newFileName = DateTime.Now.Ticks + "_" + Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            Directory.CreateDirectory(_guacHomePath);
            var filePath = Path.Combine(_guacHomePath, newFileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            return filePath;
        }

        internal bool IsSupportedTypes(string fileNameWithExt)
        {
            switch(Path.GetExtension(fileNameWithExt))
            {
                case "pdf":
                    return true;
                default:
                    return false;
            }
        }
    }
}
