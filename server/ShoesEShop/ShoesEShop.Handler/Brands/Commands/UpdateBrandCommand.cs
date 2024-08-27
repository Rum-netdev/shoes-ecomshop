using Microsoft.EntityFrameworkCore;
using ShoesEShop.Data;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Shared;

namespace ShoesEShop.Handler.Brands.Commands
{
    public class UpdateBrandCommand : ICommand<UpdateBrandCommandResult>
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }

    public class UpdateBrandCommandHandler : ICommandHandler<UpdateBrandCommand, UpdateBrandCommandResult>
    {
        private readonly ApplicationDbContext _db;

        public UpdateBrandCommandHandler(
            ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<UpdateBrandCommandResult> Handle(UpdateBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _db.Brands
                .Where(t => t.Id == request.BrandId)
                .FirstOrDefaultAsync();

            if(brand != null)
            {
                brand.Name = request.Name;
                brand.Description = request.Description;
                await _db.SaveChangesAsync();

                return new UpdateBrandCommandResult
                {
                    IsSucceed = true,
                    BrandId = brand.Id,
                    Message = "Update brand successfully"
                };
            }

            return new UpdateBrandCommandResult
            {
                BrandId = request.BrandId,
                IsSucceed = true,
                Message = "Brand is not existing"
            };
        }
    }

    public class UpdateBrandCommandResult : BaseResult
    {
        public int BrandId { get; set; }
    }
}
