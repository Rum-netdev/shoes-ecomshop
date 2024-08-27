using EFCore.BulkExtensions;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using ShoesEShop.Data;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Services.Interfaces;
using ShoesEShop.Handler.Shared;

namespace ShoesEShop.Handler.Brands.Commands
{
    public class BulkInsertBrandsCommand : ICommand<BulkInsertBrandsCommandResult>
    {
        public IFormFile BrandsFile { get; set; }
    }

    public class BulkInsertBrandsCommandHandler : ICommandHandler<BulkInsertBrandsCommand, BulkInsertBrandsCommandResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IFileService _fileService;

        public BulkInsertBrandsCommandHandler(
            ApplicationDbContext db,
            IFileService fileService)
        {
            _db = db;
            _fileService = fileService;
        }

        public async Task<BulkInsertBrandsCommandResult> Handle(BulkInsertBrandsCommand request, CancellationToken cancellationToken)
        {
            int largestBrandId = (await _db.Brands
                .Select(t => t.Id)
                .ToListAsync())
                .DefaultIfEmpty(0)
                .Max();

            var text = await _fileService.GetTextListFromHttpFileAsync(request.BrandsFile);
            var textHeader = text.First();
            text.Remove(textHeader);

            var brands = text
                .Select(t =>
                {
                    var rowData = t.Split(',');
                    return new Brand
                    {
                        Id = ++largestBrandId,
                        Name = rowData[0],
                        Description = rowData[1],
                    };
                })
                .ToList();

            try
            {
                await _db.BulkInsertAsync(brands, cancellationToken: cancellationToken);
            }
            catch (Exception ex)
            {
                return new BulkInsertBrandsCommandResult
                {
                    IsSucceed = false,
                    Message = ex.Message,
                    TotalSuccess = 0,
                };
            }

            return new BulkInsertBrandsCommandResult
            {
                IsSucceed = true,
                Message = "Insert brand successfully",
                TotalSuccess = brands.Count,
            };
        }
    }

    public class BulkInsertBrandsCommandResult : BaseResult
    {
        public int TotalSuccess { get; set; }
    }
}
