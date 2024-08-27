using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoesEShop.Data;
using ShoesEShop.Data.Entities;
using ShoesEShop.Handler.Brands.Dtos;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Shared;

namespace ShoesEShop.Handler.Brands.Commands
{
    public class DeleteBrandCommand : ICommand<DeleteBrandCommandResult>
    {
        public int BrandId { get; set; }
        public DeleteBrandCommand()
        {
        }

        public DeleteBrandCommand(int id) => BrandId = id;
    }

    public class DeleteBrandCommandHandler : ICommandHandler<DeleteBrandCommand, DeleteBrandCommandResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public DeleteBrandCommandHandler(
            ApplicationDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<DeleteBrandCommandResult> Handle(DeleteBrandCommand request, CancellationToken cancellationToken)
        {
            var brand = await _db.Brands
                .Where(t => t.Id == request.BrandId)
                .FirstOrDefaultAsync();

            if(brand != null)
            {
                var dtos = _mapper.Map<BrandDto>(brand);
                _db.Brands.Remove(brand);
                await _db.SaveChangesAsync();
                return new DeleteBrandCommandResult
                {
                    Data = dtos,
                    IsSucceed = true,
                    Message = $"Remove {nameof(Brand)} successfully"
                };
            }

            return new DeleteBrandCommandResult
            {
                IsSucceed = true,
                Message = $"The brand has ID {request.BrandId} is not existing"
            };
        }
    }

    public class DeleteBrandCommandResult : BaseResult<BrandDto>
    {
    }
}
