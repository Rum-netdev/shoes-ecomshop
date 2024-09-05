using Microsoft.EntityFrameworkCore;
using ShoesEShop.Data;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Shared;
using System.Linq;

namespace ShoesEShop.Handler.Products.Commands
{
    public class DeleteProductCommand : ICommand<DeleteProductCommandResult>
    {
        public int ProductId { get; set; }
    }

    public class DeleteProductCommandHandler : ICommandHandler<DeleteProductCommand, DeleteProductCommandResult>
    {
        private readonly ApplicationDbContext _db;

        public DeleteProductCommandHandler(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<DeleteProductCommandResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var product = await _db.Products.Where(t => t.Id == request.ProductId)
                .Include(x => x.ProductImages)
                .FirstOrDefaultAsync();

            if (product == null)
                return new DeleteProductCommandResult
                {
                    IsSucceed = true,
                    Message = $"There's no product has ID {request.ProductId}"
                };

            _db.Products.Remove(product);
            await _db.SaveChangesAsync();

            return new DeleteProductCommandResult
            {
                IsSucceed = true,
                Message = "Delete product successfully",
                ProductId = request.ProductId
            };
        }
    }

    public class DeleteProductCommandResult : BaseResult
    {
        public int ProductId { get; set; }
    }
}
