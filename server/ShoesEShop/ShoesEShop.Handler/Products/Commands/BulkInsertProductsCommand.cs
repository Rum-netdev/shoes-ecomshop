using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using ShoesEShop.Data;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Services.Interfaces;
using ShoesEShop.Handler.Shared;

namespace ShoesEShop.Handler.Products.Commands
{
    public class BulkInsertProductsCommand : ICommand<BulkInsertProductCommandResult>
    {
        public IFormFile ProductsFile { get; set; }
    }

    public class BulkInsertProductsCommandHandler : ICommandHandler<BulkInsertProductsCommand, BulkInsertProductCommandResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileService _fileService;

        public BulkInsertProductsCommandHandler(
            ApplicationDbContext db,
            IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }

        public async Task<BulkInsertProductCommandResult> Handle(BulkInsertProductsCommand request, CancellationToken cancellationToken)
        {
            int largestEntityId = _db.Products
                .Select(p => p.Id)
                .ToList()
                .DefaultIfEmpty(0)
                .Max();

            BulkInsertProductCommandResult result = new();
            var rows = await _fileService.GetTextListFromHttpFileAsync(request.ProductsFile);
            string header = rows.First();
            rows.Remove(header);  // remove first item, which represent for header

            var products = rows
                .Select(t =>
                {
                    var values = t.Split(',');
                    return new Product
                    {
                        Id = ++largestEntityId,
                        ProductName = values[0],
                        Description = values[1],
                        Quantity = int.Parse(values[2]),
                        Price = decimal.Parse(values[3])
                    };
                })
                .ToList();

            try
            {
                await _db.BulkInsertAsync(products, options =>
                {
                    //options.PropertiesToExclude = new() { nameof(Product.Id) };
                });
            }
            catch (Exception ex) 
            {
                result.IsSucceed = false;
                result.Message = ex.Message;
                return result;
            }

            result.IsSucceed = true;
            result.Message = "Insert new products successfully!";
            result.TotalSuccess = products.Count;
            return result;
        }
    }

    public class BulkInsertProductCommandResult : BaseResult
    {
        public int TotalSuccess { get; set; }
    }
}
