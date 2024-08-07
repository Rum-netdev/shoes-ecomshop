using ShoesEShop.Data;
using ShoesEShop.Handler.Infrastructures;
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
        public List<int> CategoriesId { get; set; }
    }

    public class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductCommandResult>
    {
        private readonly ApplicationDbContext _db;

        public CreateProductCommandHandler(
            ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<CreateProductCommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //var brand = _db.Brands.Where(t => t.Id == request.BrandId).FirstOrDefault();
            //var areCategoriesExisting = _db.Categories
            //    .Select(t => t.Id);

            throw new NotImplementedException();
        }
    }

    public class CreateProductCommandResult : BaseResult
    {
        public int ProductId { get; set; }
    }
}
