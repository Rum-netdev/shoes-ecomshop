using Microsoft.EntityFrameworkCore;
using ShoesEShop.Data;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesEShop.Handler.Categories.Commands
{
    public class CreateCategoryCommand : ICommand<CreateCategoryCommandResult>
    {
        public string CategoryName { get; set; }
        public string Description { get; set; }
        public int[] Products { get; set; }
    }

    public class CreateCategoryCommandHandler : ICommandHandler<CreateCategoryCommand, CreateCategoryCommandResult>
    {
        private readonly ApplicationDbContext _db;

        public CreateCategoryCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CreateCategoryCommandResult> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = new Category
            {
                CategoryName = request.CategoryName,
                Description = request.Description,
            };

            _db.Categories.Add(category);

            var productQuery = _db.Products.AsQueryable();
            foreach(var productId in request.Products)
            {
                productQuery.Where(t => t.Id == productId);
            }
            var products = await productQuery.ToListAsync();

            var productCategories = products
                .Select(x => new ProductCategory
                {
                    Category = category,
                    Product = x
                })
                .ToList();

            _db.ProductCategories.AddRange(productCategories);
            await _db.SaveChangesAsync();

            return new CreateCategoryCommandResult
            {
                CategoryId = category.Id,
                IsSucceed = true,
                Message = "Create category successfully"
            };
        }
    }

    public class CreateCategoryCommandResult : BaseResult
    {
        public int CategoryId { get; set; }
    }
}
