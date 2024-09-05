using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoesEShop.Data;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Products.Dtos;
using ShoesEShop.Handler.Services.Interfaces;
using ShoesEShop.Handler.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesEShop.Handler.Products.Commands
{
    public class UpdateProductCommand : ICommand<UpdateProductCommandResult>
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public int? BrandId { get; set; }
        public IFormFileCollection Images { get; set; }
    }

    public class UpdateProductCommandHandler : ICommandHandler<UpdateProductCommand, UpdateProductCommandResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;
        private readonly IFileStorageService _fileStorage;

        public UpdateProductCommandHandler(ApplicationDbContext db,
            IMapper mapper,
            IFileStorageService fileStorage)
        {
            _db = db;
            _mapper = mapper;
            _fileStorage = fileStorage;
        }

        public async  Task<UpdateProductCommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _db.Products
                .Where(t => t.Id == request.Id)
                .FirstOrDefaultAsync();

            if (product == null)
                return new UpdateProductCommandResult
                {
                    IsSucceed = false,
                    Message = "Product is not existing!",
                    ProductId = request.Id
                };

            //var newProduct = _mapper.Map<Product>(request);
            product.ProductName = request.ProductName;
            product.Description = request.Description;
            product.Price = request.Price;
            product.Quantity = request.Quantity;

            if(request.BrandId.HasValue)
            {
                var brand = await _db.Brands
                    .Where(t => t.Id == request.BrandId)
                    .FirstOrDefaultAsync();
                product.Brand = brand;
            }

            if(request.Images != null)
            {
                List<string> imageUrls = new List<string>();
                foreach(var image in request.Images)
                {
                    imageUrls.Add(await _fileStorage.SaveFileAsync(image));
                }
                product.ProductImages = imageUrls
                    .Select(x => new ProductImage
                    {
                        Urls = x
                    })
                    .ToList();
            }

            _db.Products.Update(product);
            await _db.SaveChangesAsync();
            return new UpdateProductCommandResult
            {
                ProductId = request.Id,
                IsSucceed = true,
                Message = "Update product successfully"
            };
        }
    }

    public class UpdateProductCommandResult : BaseResult
    {
        public int ProductId { get; set; }
    }
}
