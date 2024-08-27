using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoesEShop.Data;
using ShoesEShop.Handler.Brands.Dtos;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Shared.Pagination;

namespace ShoesEShop.Handler.Brands.Queries
{
    public record GetAllBrandsQuery() : IQuery<GetAllBrandsQueryResult>;

    public class GetAllBrandsQueryHandler : IQueryHandler<GetAllBrandsQuery, GetAllBrandsQueryResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GetAllBrandsQueryHandler(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<GetAllBrandsQueryResult> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
        {
            var brands = await _db.Brands.ToListAsync();
            if (brands.Count <= 0)
                return new GetAllBrandsQueryResult(new List<BrandDto>())
                {
                    IsSucceed = true,
                };

            var brandDtos = _mapper.Map<List<BrandDto>>(brands);
            return new GetAllBrandsQueryResult(brandDtos)
            {
                IsSucceed = true,
                TotalRecords = brands.Count
            };
        }
    }

    public class GetAllBrandsQueryResult : PaginationResult<BrandDto>
    {
        public GetAllBrandsQueryResult(List<BrandDto> data)
            :base(data)
        {
        }

        public GetAllBrandsQueryResult(List<BrandDto> data, int pageSize, int pageCount, int totalRecords)
            : base(data, pageSize, pageCount, totalRecords)
        {
        }
    }
}
