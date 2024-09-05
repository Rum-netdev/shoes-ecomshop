using AutoMapper;
using Microsoft.AspNetCore.Http;
using ShoesEShop.Data;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Services.Interfaces;
using ShoesEShop.Handler.Shared;

namespace ShoesEShop.Handler.Products.Commands
{
    public class CreateProductCommand : ICommand<CreateProductCommandResult>
    {
        public string ProductName { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        public List<int>? CategoriesId { get; set; }
        public IFormFileCollection Images { get; set; }
    }

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorage;

        public CreateProductCommandHandler(
            ApplicationDbContext db,
            IMapper mapper,
            IFileStorageService fileStorage)
        {
            _db = db;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public async Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var brand = _db.Brands.Where(t => t.Id == request.BrandId).FirstOrDefault();
            var product = new Product
            {
                ProductName = request.ProductName,
                Description = request.Description,
                Price = request.Price,
                Quantity = request.Quantity,
                Brand = brand
            };

            List<string> imageUrls = new List<string>();
            foreach (var image in request.Images)
            {
                imageUrls.Add(await _fileStorage.SaveFileAsync(image));
            }

            product.ProductImages =
                imageUrls.Select(x => new ProductImage
                {
                    Urls = x
                }).ToList();

            _db.Products.Add(product);
            await _db.SaveChangesAsync();

            return new CreateProductCommandResult
            {
                IsSucceed = true,
                Message = "Create product successfully",
                ProductId = product.Id
            };
        }
    }

    public class CreateProductCommandResult : BaseResult
    {
        public int ProductId { get; set; }
    }
}
