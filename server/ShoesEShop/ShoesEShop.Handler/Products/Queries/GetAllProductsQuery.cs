using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ShoesEShop.Data;
using ShoesEShop.Handler.Infrastructures;
using ShoesEShop.Handler.Products.Dtos;
using ShoesEShop.Handler.Shared.Pagination;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShoesEShop.Handler.Products.Queries
{
    public record GetAllProductsQuery() : IQuery<GetAllProductsQueryResult>;

    public class GetAllProductsQueryHandler : IQueryHandler<GetAllProductsQuery, GetAllProductsQueryResult>
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public GetAllProductsQueryHandler(
            ApplicationDbContext db,
            IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<GetAllProductsQueryResult> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await _db.Products.ToListAsync();
            if (products.Count == 0)
                return new GetAllProductsQueryResult(null);

            var productDtos = _mapper.Map<List<ProductDto>>(products);
            return new GetAllProductsQueryResult(productDtos);
        }
    }

    public class GetAllProductsQueryResult : PaginationResult<ProductDto>
    {
        public GetAllProductsQueryResult(List<ProductDto> data):base(data)
        {
        }

        public GetAllProductsQueryResult(List<ProductDto> data, int pageSize, int pageCount, int totalRecords)
            :base(data, pageSize, pageCount, totalRecords)
        {
        }
    }
}
