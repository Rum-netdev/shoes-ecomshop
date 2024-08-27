using ShoesEShop.Data;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Shared;

namespace ShoesEShop.Handler.Brands.Commands
{
    public class CreateBrandCommand : ICommand<CreateBrandCommandResult>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class CreateBrandCommandHandler : ICommandHandler<CreateBrandCommand, CreateBrandCommandResult>
    {
        private readonly ApplicationDbContext _db;

        public CreateBrandCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<CreateBrandCommandResult> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = new Brand()
            {
                Name = request.Name,
                Description = request.Description,
            };
            _db.Brands.Add(brand);
            await _db.SaveChangesAsync();

            return new CreateBrandCommandResult
            {
                IsSucceed = true,
                Message = "Brand has been created successfully",
                BrandId = brand.Id
            };
        }
    }

    public class CreateBrandCommandResult : BaseResult
    {
        public int BrandId { get; set; }
    }
}
