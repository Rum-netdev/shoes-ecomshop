using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoesEShop.Data;
using ShoesEShop.Handler.Brands.Dtos;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Shared;

namespace ShoesEShop.Handler.Brands.Queries
{
    public record GetBrandByIdQuery(int BrandId) : IQuery<GetBrandByIdQueryResult>;

    public class GetBrandByIdQueryHandler : IQueryHandler<GetBrandByIdQuery, GetBrandByIdQueryResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GetBrandByIdQueryHandler(
            ApplicationDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GetBrandByIdQueryResult> Handle(GetBrandByIdQuery request, CancellationToken cancellationToken)
        {
            var brand = await _db.Brands
                .Where(t => t.Id == request.BrandId)
                .FirstOrDefaultAsync();

            if (brand == null)
                return new GetBrandByIdQueryResult
                {
                    IsSucceed = false,
                    Message = $"Brand with Id {request.BrandId} does not exist"
                };

            var dto = _mapper.Map<BrandDto>(brand);
            return new GetBrandByIdQueryResult
            {
                Data = dto,
                IsSucceed = true
            };
        }
    }

    public class GetBrandByIdQueryResult : BaseResult<BrandDto>
    {
    }
}
